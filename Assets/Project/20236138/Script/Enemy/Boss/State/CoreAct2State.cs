using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAct2State : ModeStateBase
{
    public CoreAct2State(IStateMachine _stateMachine, CoreEnemy enemy) : base(_stateMachine)
    {
        _enemy = enemy;
    }
    private CoreEnemy _enemy;

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
