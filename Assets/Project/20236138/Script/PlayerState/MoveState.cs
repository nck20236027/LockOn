using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if(_player.isBoostButton)
        {
            stateMachine.ChangeState(ModeStateType.Boost);
        }
        if (_player.isDecelerationButton)
        {
            stateMachine.ChangeState(ModeStateType.Deceleration);
        }

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
