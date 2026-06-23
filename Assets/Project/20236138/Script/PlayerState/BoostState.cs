using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// ブースト（Boost）状態を表すステートクラス。
/// ブースト開始時のカメラ切替、UI 更新、サウンド制御を行い、経過に応じて移動ロジック（RocketMove）で推進する。
/// 一定時間後にブースト効果音を再生し、ボタン解放や燃料切れで状態を遷移させる。
/// </summary>
public class BoostState : ModeStateBase
{
    private float _stateChangedTime = 0;
    private Player _player;
    private PlayerMoveStatus _status;
    private CancellationTokenSource _enterCancellation;
    public override ModeStateType StateType => ModeStateType.Boost;
    
    public BoostState(IStateMachine stateMachine, Player player) : base(stateMachine)
    {
        _player = player;
        _status = player.BoostStatus;
    }

    /// <summary>
    /// Boost状態に入り、遅延後のSE切り替え処理をキャンセル可能な非同期処理として開始する。
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();

        _player.cameraController.Interface.CameraChange();
        _player.GetRigidbody.velocity = Vector3.zero;

        // ターゲットが存在する場合は向き合わせる
        if (_player.GetTargetVector != Vector3.zero)
        {
            Vector3 diffDir = (_player.GetTargetVector - _player.transform.position).normalized;
            _player.transform.rotation = Quaternion.FromToRotation(Vector3.up, diffDir);
        }
        
        // UI 更新：ボタン入力状態を反映
        _player.energyGaugeParam.buttonState = ButtonState._isInputDown;
        _player.energyGaugeParam.energyTimeLost = _status.FuelConsumption * Time.deltaTime;
        ServiceLocator<UIMediator>.GetInstance().Reload(_player.energyGaugeParam);
        _player.energyGaugeParam.buttonState = ButtonState._isInputNow;
        ServiceLocator<SEManager>.GetInstance().StopSound(_player.RocketSound.RocketFlightSound);

        // 一定時間経過後にブースト音再生と飛行音切り替え
        CancelEnterTask();
        _enterCancellation = CancellationTokenSource.CreateLinkedTokenSource(_player.destroyCancellationToken);
        PlayBoostSoundAsync(_enterCancellation.Token).Forget();
    }

    /// <summary>
    /// ブースト停止時間の経過後にSEを切り替える。
    /// </summary>
    private async UniTask PlayBoostSoundAsync(CancellationToken cancellationToken)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_player.BoostStopTime), cancellationToken: cancellationToken);
            ServiceLocator<SEManager>.GetInstance().PlaySound(_player.RocketSound.BoostSound, true);
            ServiceLocator<SEManager>.GetInstance().PlaySound(_player.RocketSound.RocketFlightSound, false,true);
            _player.energyGaugeParam.buttonState = ButtonState._isInputNow;
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

    public override void OnUpdate()
    {
        _player.TryUseFuel(_status.FuelConsumption);
        if(_stateChangedTime > _player.BoostStatusUnChangeTime&& !_player.isBoostButton)
        {
            stateMachine.ChangeState(ModeStateType.Move);
        }
        if (_player.FuelQuantity < 0)
        {
            stateMachine.ChangeState(ModeStateType.Death);
        }
        ServiceLocator<UIMediator>.GetInstance().Reload(_player.energyGaugeParam);
    }


    public override void OnFixedUpdate()
    {
        _stateChangedTime += Time.fixedDeltaTime;
        MoveTarget();
    }

    public override void OnExit()
    {
        CancelEnterTask();
        _player.cameraController.Interface.CameraChange();
        _stateChangedTime = 0;
        _player.energyGaugeParam.buttonState = ButtonState._isInputUp;
        ServiceLocator<UIMediator>.GetInstance().Reload(_player.energyGaugeParam);
    }

    /// <summary>
    /// Boost状態で開始した非同期処理を停止して参照を解放する。
    /// </summary>
    private void CancelEnterTask()
    {
        if (_enterCancellation == null) return;

        _enterCancellation.Cancel();
        _enterCancellation.Dispose();
        _enterCancellation = null;
    }

    private void MoveTarget()
    {
        RocketMove.MoveTarget(_player, _status, _stateChangedTime);
    }
}
