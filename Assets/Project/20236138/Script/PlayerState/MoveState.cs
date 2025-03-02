using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class MoveState : ModeStateBase
{
    float _stateChangedTime = 0;
    private Player _player;
    private PlayerMoveStatus _state;
    public MoveState(IStateMachine _stateMachine,Player _player) : base(_stateMachine)
    {
        this._player = _player;
        this._state = this._player.normalState;
    }

    public override ModeStateType StateType => ModeStateType.Move;

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log(this.ToString());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        float timeFuelQuatity = (_player.GetTarget != null ?
            _player.GetTarget.ChangeConsuptio(-_state.FuelConsumptio): -_state.FuelConsumptio) ;
        _player.FuelQuantity += timeFuelQuatity * Time.deltaTime;
        _player._energyGageParam.energyTimeLost = -timeFuelQuatity * Time.deltaTime;
        
        if(_player.isBoostButton)
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
        ServiceLocator<UIMediator>.GetInstance().Reload(_player._energyGageParam);

    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _stateChangedTime += Time.fixedDeltaTime;
        MoveTarget();
    }

    public override void OnExit()
    {
        base.OnExit();
        _stateChangedTime = 0;
    }

    private void MoveTarget()
    {
        RocetMove.MoveTarget(_player, _state, _stateChangedTime);
    }
}
