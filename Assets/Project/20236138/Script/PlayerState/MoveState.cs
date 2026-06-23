using UnityEngine;

/// <summary>
/// 通常移動（Move）状態を表すステートクラス。
/// プレイヤーの燃料消費や入力による状態遷移（ブースト・減速・死亡）を扱う。
/// 固定更新では RocketMove を使って物理的な移動を行う。
/// </summary>
public class MoveState : ModeStateBase
{
    private float _stateChangedTime;
    private Player _player;
    private PlayerMoveStatus _status;
    public override ModeStateType StateType => ModeStateType.Move;
    
    public MoveState(IStateMachine stateMachine,Player player) : base(stateMachine)
    {
        _player = player;
        _status = _player.NormalStatus;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {   
        _player.TryUseFuel(_status.FuelConsumption);
        
        if(_stateChangedTime > _player.BoostStatusUnChangeTime &&_player.isBoostButton)
        {
            stateMachine.ChangeState(ModeStateType.Boost);
        }
        if (_player.isDecelerationButton)
        {
            stateMachine.ChangeState(ModeStateType.Deceleration);
        }
        if (_player.FuelQuantity < 0)
        {
            stateMachine.ChangeState(ModeStateType.Death);
        }
        ServiceLocator<UIMediator>.GetInstance().Animation(_player.energyGaugeParam);

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

    // 実際の移動ロジック呼び出し（RocketMove に委譲）
    private void MoveTarget()
    {
        RocketMove.MoveTarget(_player, _status, _stateChangedTime);
    }
}
