using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// ボスの Act3（敵生成）状態。
/// - 定期的に小型敵をスポーンして行動させる。
/// </summary>
public class CoreAct3State : EnemyModeStateBase
{
    private CancellationTokenSource _enterCancellation;
    
    private CoreEnemy _enemy;
    
    public override CoreEnemyState StateType => CoreEnemyState.Act3;
    
    public CoreAct3State(IMadeStateMachine stateMachine, CoreEnemy enemy) : base(stateMachine)
    {
        _enemy = enemy;
    }
    
    /// <summary>
    /// Act3状態に入り、小型敵生成をキャンセル可能な非同期処理として開始する。
    /// </summary>
    public override void OnEnter()
    {
        TargetManager.Instance.AddLockTarget(_enemy);
        _enemy.IsAttack = true;

        CancelEnterTask();
        _enterCancellation = new CancellationTokenSource();
        EnterAsync(_enterCancellation.Token, _enemy.Token).Forget();
    }

    /// <summary>
    /// Act3の小型敵生成と待機時間を順に処理する。
    /// </summary>
    private async UniTask EnterAsync(CancellationToken exitToken, CancellationToken enemyToken)
    {
        CancellationTokenSource spawnCancellation = new();
        CancellationTokenSource sequenceTokenSource = CancellationTokenSource.CreateLinkedTokenSource(exitToken, enemyToken);
        CancellationTokenSource spawnTokenSource = CancellationTokenSource.CreateLinkedTokenSource(sequenceTokenSource.Token, spawnCancellation.Token);
        
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.AnimationTime), cancellationToken: sequenceTokenSource.Token);
            CreateTriangleEnemy(spawnTokenSource.Token).SuppressCancellationThrow().Forget();

            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction3Data.Act3AttackTime), cancellationToken: sequenceTokenSource.Token);
            spawnCancellation.Cancel();
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction3Data.Act3ChanceTime), cancellationToken: sequenceTokenSource.Token);
            stateMachine.ChangeState((int)CoreEnemyState.Idle);
        }
        catch (OperationCanceledException)
        {
            if (!exitToken.IsCancellationRequested && enemyToken.IsCancellationRequested)
            {
                stateMachine.ChangeState((int)CoreEnemyState.Idle);
            }
        }
        finally
        {
            spawnCancellation.Cancel();
            spawnTokenSource.Dispose();
            sequenceTokenSource.Dispose();
            spawnCancellation.Dispose();
        }
    }

    public override void OnExit()
    {
        CancelEnterTask();
        TargetManager.Instance.RemoveLockTarget(_enemy);
        _enemy.IsAttack = false;
    }

    /// <summary>
    /// Act3用の小型敵をキャンセルされるまで生成し続ける。
    /// </summary>
    private async UniTask CreateTriangleEnemy(CancellationToken token)
    {
        while(!token.IsCancellationRequested)
        {
            float _enemyRotation = 360f * (UnityEngine.Random.Range(0f,10f) / 10);
            _enemyRotation %= 360;
            Quaternion _rotation = _enemy.transform.rotation * Quaternion.Euler(0, _enemyRotation, 0);
            Vector3 _vector = _enemy.transform.position + _rotation * Vector3.forward * _enemy.CoreEnemyAction3Data.Act3EnemyCreateDistance;
            _enemy.CreateTriangleEnemy(_vector, Quaternion.Euler(0, _enemyRotation, 0));
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction3Data.Act3CreateEnemyInterval), cancellationToken: token);
        }
    }

    /// <summary>
    /// Act3中に開始した非同期処理を停止して参照を解放する。
    /// </summary>
    private void CancelEnterTask()
    {
        if (_enterCancellation == null) return;

        _enterCancellation.Cancel();
        _enterCancellation.Dispose();
        _enterCancellation = null;
    }
}
