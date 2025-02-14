using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : IStateMachine
{
    private Dictionary<ModeStateType, ModeStateBase> stateDic = new Dictionary<ModeStateType, ModeStateBase>();

    private ModeStateBase currentState;
    public ModeStateBase CurrentState => currentState;

    public StateMachine(Player player)
    {
        

        stateDic.Add(ModeStateType.Deceleration, ModeStateFactory.Create(ModeStateType.Deceleration,this,player));
        stateDic.Add(ModeStateType.Move, ModeStateFactory.Create(ModeStateType.Move, this, player));
        stateDic.Add(ModeStateType.Boost, ModeStateFactory.Create(ModeStateType.Boost, this, player));
        stateDic.Add(ModeStateType.BeforBoost, ModeStateFactory.Create(ModeStateType.BeforBoost, this, player));
    }

    public void ChangeState(ModeStateType changeStateType)
    {
        currentState.OnExit();

        currentState = stateDic[changeStateType];

        currentState.OnEnter();
    }

    public void Initialize(ModeStateType initStateType)
    {
        currentState = stateDic[initStateType];

    }

    public void OnEnter()
    {
        currentState.OnEnter();
    }

    public void OnUpdate()
    {
        currentState.OnUpdate();
    }

    public void OnFixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    public void OnExit()
    {
        currentState.OnExit();
    }
}

public enum ModeStateType
{
    BeforBoost,
    Boost,
    Move,
    Deceleration,

}

public static class ModeStateFactory
{
    public static ModeStateBase Create(ModeStateType stateType, StateMachine modeStateContext,Player player)
    {
        switch (stateType)
        {

            case ModeStateType.Move: return new MoveState(modeStateContext , player);

            case ModeStateType.Deceleration: return new DecelerationState(modeStateContext,player);
            

            case ModeStateType.Boost: return new BoostState(modeStateContext, player);
        }

        return null;
    }
}
