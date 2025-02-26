using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyModeStateBase
{
    //ステートマシン
    protected IMadeStateMachine stateMachine;

    public abstract ModeStateType StateType { get; }

    public EnemyModeStateBase(IMadeStateMachine _stateMachine)
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