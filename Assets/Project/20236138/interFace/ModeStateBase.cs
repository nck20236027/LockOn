using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModeStateBase
{
    //ステートマシン
    protected IStateMachine stateMachine;

    public abstract ModeStateType StateType { get; }

    public ModeStateBase(IStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }
    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}