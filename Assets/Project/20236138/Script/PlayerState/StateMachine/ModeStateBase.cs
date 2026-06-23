using UnityEngine;

public abstract class ModeStateBase
{
    protected IStateMachine stateMachine;

    public abstract ModeStateType StateType { get; }

    public ModeStateBase(IStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public virtual void OnEnter()
    {
        #if UNITY_EDITOR
        
        Debug.Log(ToString());
        
        #endif
    }

    public virtual void OnUpdate(){}

    public virtual void OnFixedUpdate(){}

    public virtual void OnExit(){}
}
