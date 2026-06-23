using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// 死亡（Death）状態を表すステートクラス。
/// プレイヤーの物理挙動を停止し、待機時間の後にゲームオーバー処理を行う。
/// </summary>
public class DeathState : ModeStateBase
{
    private Player _player;
    private CancellationTokenSource _enterCancellation;
    public override ModeStateType StateType => ModeStateType.Death;
    
    public DeathState(IStateMachine stateMachine,Player player) : base(stateMachine)
    {
        _player = player;
    }

    /// <summary>
    /// 死亡状態を示す列挙値を返す
    // ステートに入るとカメラ停止、物理の有効化（落下）を行い、待機後にゲームオーバー処理を発行する
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        _player.cameraController.Interface.CameraStop();
        _player.GetRigidbody.isKinematic = false;
        _player.GetRigidbody.useGravity = true;

        CancelEnterTask();
        _enterCancellation = CancellationTokenSource.CreateLinkedTokenSource(_player.destroyCancellationToken);
        ShowGameOverAsync(_enterCancellation.Token).Forget();
    }

    public override void OnExit()
    {
        CancelEnterTask();
    }

    /// <summary>
    /// 指定の待機時間後にUIとハンドラをゲームオーバー用に更新する。
    /// </summary>
    private async UniTask ShowGameOverAsync(CancellationToken cancellationToken)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_player.StandbyTime),cancellationToken:cancellationToken);
            ServiceLocator<UIMediator>.GetInstance().Init(_player.gameOverParam);
            ServiceLocator<UIMediator>.GetInstance().Show(_player.gameOverParam);
            ServiceLocator<UIMediator>.GetInstance().Reload(_player.gameOverParam);
            ServiceLocator<HandlerController>.GetInstance().HandlersDisable();
            ServiceLocator<HandlerController>.GetInstance().GameoverHandler();
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

    /// <summary>
    /// Death状態で開始した非同期処理を停止して参照を解放する。
    /// </summary>
    private void CancelEnterTask()
    {
        if (_enterCancellation == null) return;

        _enterCancellation.Cancel();
        _enterCancellation.Dispose();
        _enterCancellation = null;
    }
}
