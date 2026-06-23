using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// ボスの Act2（ライン攻撃やプレイヤー追跡）状態。
/// - 範囲内のプレイヤーを検出して回転・弾発射を行う。
/// </summary>
public class CoreAct2State : EnemyModeStateBase
{
    private float _attackTime = 0f;
    private CancellationTokenSource _enterCancellation;
    private CoreEnemy _enemy;
    public override CoreEnemyState StateType => CoreEnemyState.Act2;

    public CoreAct2State(IMadeStateMachine stateMachine, CoreEnemy enemy) : base(stateMachine)
    {
        _enemy = enemy;
    }

    /// <summary>
    /// Act2状態に入り、攻撃シーケンスをキャンセル可能な非同期処理として開始する。
    /// </summary>
    public override void OnEnter()
    {
        _enemy.LineRenderer.SetPosition(0,_enemy.transform.position);
        _enemy.LineRenderer.enabled = false;
        _attackTime = 0f;
        TargetManager.Instance.AddLockTarget(_enemy);
        _enemy.IsAttack = true;

        CancelEnterTask();
        _enterCancellation = new CancellationTokenSource();
        EnterAsync(_enterCancellation.Token, _enemy.Token).Forget();
    }

    /// <summary>
    /// Act2の弾生成と待機時間を順に処理する。
    /// </summary>
    private async UniTask EnterAsync(CancellationToken exitToken, CancellationToken enemyToken)
    {
        CancellationTokenSource attackCancellation = new();
        CancellationTokenSource sequenceTokenSource = CancellationTokenSource.CreateLinkedTokenSource(exitToken, enemyToken);
        CancellationTokenSource bulletTokenSource = CancellationTokenSource.CreateLinkedTokenSource(sequenceTokenSource.Token, attackCancellation.Token);
        
        try
        {
            // Act1と同様のシーケンスで、攻撃開始前の待機、弾生成、攻撃終了後の待機を行う
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.AnimationTime),cancellationToken: sequenceTokenSource.Token);
            CreateBullet(bulletTokenSource.Token).SuppressCancellationThrow().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction2Data.Act2AttackTime), cancellationToken: sequenceTokenSource.Token);
            attackCancellation.Cancel();
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction2Data.Act2ChanceTime), cancellationToken: sequenceTokenSource.Token);
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
            attackCancellation.Cancel();
            bulletTokenSource.Dispose();
            sequenceTokenSource.Dispose();
            attackCancellation.Dispose();
        }
    }

    public override void OnExit()
    {
        CancelEnterTask();
        TargetManager.Instance.RemoveLockTarget(_enemy);
        _enemy.IsAttack = false;
    }

    public override void OnFixedUpdate()
    {
        // プレイヤーが範囲内にいる場合はプレイヤーの方向を向き、ラインレンダラーで攻撃範囲を表示する
        //一定のペースでプレイヤーに照準を合わせる
        _attackTime += Time.fixedDeltaTime;
        if ((_enemy.transform.position - TargetManager.Instance.GetPlayerPos).sqrMagnitude < Mathf.Pow(_enemy.CoreEnemyAction2Data.SearchDistance, 2))
        {
            Quaternion targetRotation = Quaternion.LookRotation(TargetManager.Instance.GetPlayerPos - _enemy.transform.position, Vector3.up);
            _enemy.transform.rotation = 
                Quaternion.Lerp(
                    _enemy.transform.rotation, 
                    targetRotation, 
                    Time.fixedDeltaTime * _enemy.CoreEnemyAction2Data.Act2RotationSpeed
                    );
            _enemy.LineRenderer.enabled = true;
            _enemy.LineRenderer.SetPosition(1,  _enemy.transform.position);
        }
        else
        {
            _enemy.LineRenderer.enabled = false;
        }
    }

    /// <summary>
    /// Act2用の弾をプレイヤーに向かって扇上にキャンセルされるまで生成し続ける。
    /// </summary>
    private async UniTask CreateBullet(CancellationToken token)
    {
        var data = _enemy.CoreEnemyAction2Data;
        
        while (!token.IsCancellationRequested)
        {
            await UniTask.Delay(0, cancellationToken: token);

            if ((_enemy.transform.position - TargetManager.Instance.GetPlayerPos).sqrMagnitude < Mathf.Pow(_enemy.CoreEnemyAction2Data.SearchDistance, 2) ||
                _attackTime < _enemy.CoreEnemyAction2Data.EnemyBulletSpan) continue;

            _attackTime = 0;
            for (int j = 0; j < _enemy.CoreEnemyAction2Data.Act2BulletCount; j++)
            {
                for (int i = -_enemy.CoreEnemyAction2Data.Act2BulletLineCount; i <= _enemy.CoreEnemyAction2Data.Act2BulletLineCount; i++)
                {
                    Quaternion _rotation = Quaternion.Euler(0, _enemy.CoreEnemyAction2Data.EnemyBulletRotation * i, 0) * _enemy.transform.rotation;
                    Vector3 _pos = _rotation * Vector3.forward * _enemy.CoreEnemyAction2Data.EnemyBulletInstantiateDistance;
                    _enemy.BulletPool.GetBullet(_enemy.transform.position + _pos, _rotation, _enemy.CoreEnemyAction2Data.Act2BulletStatus);
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction2Data.EnemyBulletDistance), cancellationToken:token);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction2Data.EnemyBulletSpan), cancellationToken: token);
        }
    }

    /// <summary>
    /// Act2中に開始した非同期処理を停止して参照を解放する。
    /// </summary>
    private void CancelEnterTask()
    {
        if (_enterCancellation == null) return;

        _enterCancellation.Cancel();
        _enterCancellation.Dispose();
        _enterCancellation = null;
    }
}
