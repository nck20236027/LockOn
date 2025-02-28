using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : ModeStateBase
{
    Player _player;
    public DeathState(IStateMachine _stateMachine,Player _player) : base(_stateMachine)
    {
        this._player = _player;
    }

    public override ModeStateType StateType => throw new System.NotImplementedException();

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

}
