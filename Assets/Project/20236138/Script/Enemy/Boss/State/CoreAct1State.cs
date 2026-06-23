using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// ボスの Act1 状態（弾幕攻撃など）。
/// - OnEnter で攻撃開始、弾生成タスクを起動し、終了後に Idle へ戻す。
/// </summary>
public class CoreAct1State : EnemyModeStateBase
{
    private CancellationTokenSource _enterCancellation;
    private CoreEnemy _enemy;

    public override CoreEnemyState StateType => CoreEnemyState.Act1;

    public CoreAct1State(IMadeStateMachine stateMachine, CoreEnemy enemy) : base(stateMachine)
    {
        _enemy = enemy;
    }

    /// <summary>
    /// Act1状態に入り、攻撃シーケンスをキャンセル可能な非同期処理として開始する。
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
    /// Act1の弾生成と待機時間を順に処理する。
    /// </summary>
    private async UniTask EnterAsync(CancellationToken exitToken, CancellationToken enemyToken)
    {

        CancellationTokenSource attackCancellation = new();
        CancellationTokenSource sequenceTokenSource = CancellationTokenSource.CreateLinkedTokenSource(exitToken, enemyToken);
        CancellationTokenSource bulletTokenSource = CancellationTokenSource.CreateLinkedTokenSource(sequenceTokenSource.Token, attackCancellation.Token);

        try
        {
            // 攻撃開始前の待機、弾生成、攻撃終了後の待機を行う
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.AnimationTime),cancellationToken:sequenceTokenSource.Token);
            CreateBullet(bulletTokenSource.Token).SuppressCancellationThrow().Forget();

            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction1Data.Act1AttackTime), cancellationToken: sequenceTokenSource.Token);
            attackCancellation.Cancel();
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction1Data.Act1ChanceTime), cancellationToken: sequenceTokenSource.Token);
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
        _enemy.IsAttack = false;
        TargetManager.Instance?.RemoveLockTarget(_enemy);
    }

    /// <summary>
    /// Act1用の弾をキャンセルされるまで生成し続ける。
    /// </summary>
    private async UniTask CreateBullet(CancellationToken token)
    {
        float bulletCount = 0;

        while (!token.IsCancellationRequested)
        {
            // 360度に等間隔で弾を生成するため、bulletCount をインターバルに応じて増加させる
            bulletCount += 360 * (_enemy.CoreEnemyAction1Data.Act1CreateBulletInterval / _enemy.CoreEnemyAction1Data.BulletAround);
            bulletCount %= 360;
            Quaternion _rotation = _enemy.transform.rotation * Quaternion.Euler(0, bulletCount, 0);
            Vector3 _vector = _rotation * Vector3.forward;
            EnemyBulletStatus status = _enemy.CoreEnemyAction1Data.Act1BulletStatus;
            status.destroyTime = _enemy.Act1DestroyTime;
            _enemy.BulletPool.GetBullet(_enemy.transform.position + _vector * _enemy.CoreEnemyAction1Data.DistanceAttack
                , _rotation, status);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.CoreEnemyAction1Data.Act1CreateBulletInterval)
                , cancellationToken:token);
        }
    }

    /// <summary>
    /// Act1中に開始した非同期処理を停止して参照を解放する。
    /// </summary>
    private void CancelEnterTask()
    {
        if (_enterCancellation == null) return;

        _enterCancellation.Cancel();
        _enterCancellation.Dispose();
        _enterCancellation = null;
    }

    
}
