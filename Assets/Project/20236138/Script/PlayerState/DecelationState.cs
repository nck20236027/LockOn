using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecelerationState : ModeStateBase
{
    private float _stateChangedTime = 0;
    private Player _player;
    private PlayerMoveStatus _state;
    public DecelerationState(IStateMachine _stateMachine,Player _player) : base(_stateMachine)
    {
        this._player = _player;
        this._state = this._player.decelerationState;
        
    }

    public override ModeStateType StateType => ModeStateType.Deceleration;

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log(this.ToString());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!_player.isDecelerationButton)
        {
            stateMachine.ChangeState(ModeStateType.Move);
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
