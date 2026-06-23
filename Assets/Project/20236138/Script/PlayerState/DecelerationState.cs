using UnityEngine;

/// <summary>
/// 減速（Deceleration）状態を表すステートクラス。
/// プレイヤーが減速ボタンを押している間はこちらの状態になり、燃料消費や移動のロジックを制御する。
/// 固定更新で RocketMove による移動を行い、燃料が尽きたら死亡状態へ遷移する。
/// </summary>
public class DecelerationState : ModeStateBase
{
    private float _stateChangedTime = 0;
    private Player _player;
    private PlayerMoveStatus _status;
    public override ModeStateType StateType => ModeStateType.Deceleration;
    
    public DecelerationState(IStateMachine stateMachine,Player player) : base(stateMachine)
    {
        _player = player;
        _status = _player.DecelerationStatus;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log(ToString());
        // ステート開始時にエネルギー消費量を UI に反映
        _player.energyGaugeParam.energyTimeLost = _status.FuelConsumption * Time.deltaTime;
    }

    public override void OnUpdate()
    {
        _player.TryUseFuel(_status.FuelConsumption);
        
        if (!_player.isDecelerationButton)
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
        _stateChangedTime = 0;
    }

    /// <summary>
    /// ロケット移動ロジックを呼び出す（RocketMove に委譲）
    /// </summary>
    private void MoveTarget()
    {
        RocketMove.MoveTarget(_player, _status, _stateChangedTime);
    }
}
