using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// ボスの Idle（待機）状態。
/// - 一定時間待機し、その後ランダムで次の Act へ遷移する。
/// - OnEnter でウェイトとサウンドを扱う。
/// </summary>
public class CoreIdleState : EnemyModeStateBase
{
    private CoreEnemy _enemy;
    
    private CancellationTokenSource _enterCancellation;

    public override CoreEnemyState StateType => CoreEnemyState.Idle;
    
    public CoreIdleState(IMadeStateMachine stateMachine,CoreEnemy enemy) : base(stateMachine)
    {
        _enemy = enemy;
    }

    /// <summary>
    /// Idle状態に入り、待機処理をキャンセル可能な非同期処理として開始する。
    /// </summary>
    public override void OnEnter()
    {
        CancelEnterTask();
        _enterCancellation = CancellationTokenSource.CreateLinkedTokenSource(_enemy.destroyCancellationToken);
        EnterAsync(_enterCancellation.Token).Forget();
    }

    /// <summary>
    /// 待機時間後に次の攻撃状態へ遷移する。
    /// </summary>
    private async UniTask EnterAsync(CancellationToken cancellationToken)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.ActionInterval), cancellationToken:cancellationToken);
            stateMachine.ChangeState(UnityEngine.Random.Range(1,4));
            ServiceLocator<SEManager>.GetInstance().PlaySoundToPan( _enemy, _enemy.CoreStateSound,true);
        }
        catch (OperationCanceledException)
        {
            // キャンセルは正常な終了として扱うため、何もしない。
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    public override void OnExit()
    {
        CancelEnterTask();
    }

    /// <summary>
    /// Idle中に開始した非同期処理を停止して参照を解放する。
    /// </summary>
    private void CancelEnterTask()
    {
        if (_enterCancellation == null) return;

        _enterCancellation.Cancel();
        _enterCancellation.Dispose();
        _enterCancellation = null;
    }
}
