using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : IMadeStateMachine
{
    private Dictionary<int, EnemyModeStateBase> stateDic = new ();

    private EnemyModeStateBase currentState;
    public EnemyModeStateBase CurrentState => currentState;

    public EnemyStateMachine(CoreEnemy enemy)
    {
        

        
    }

    public void ChangeState(int changeStateType)
    {
        currentState.OnExit();

        currentState = stateDic[changeStateType];

        currentState.OnEnter();
    }

    public void Initialize(int initStateType)
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

public enum CoreEnemyState
{
    Idle = 0,
    Act1 = 1,
    Act2 = 2,
    Act3 = 3
}

public static class MadeModeStateFactory
{
    public static ModeStateBase Create(int stateType, EnemyStateMachine modeStateContext)
    {
        switch (stateType)
        {


        }

        return null;
    }
}
