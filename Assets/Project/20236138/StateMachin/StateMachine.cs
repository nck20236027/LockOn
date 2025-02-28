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
        stateDic.Add(0,MadeModeStateFactory.Create(0,this, enemy));
        stateDic.Add(1, MadeModeStateFactory.Create(1, this, enemy));
        stateDic.Add(2, MadeModeStateFactory.Create(2, this, enemy));
        stateDic.Add(3, MadeModeStateFactory.Create(3, this, enemy));
        stateDic.Add(4, MadeModeStateFactory.Create(4, this, enemy));

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
    Act3 = 3,
    Death = 4,
}

public static class MadeModeStateFactory
{
    public static EnemyModeStateBase Create(int stateType, EnemyStateMachine modeStateContext,CoreEnemy enemy)
    {
        switch (stateType)
        {
            case 0:
                return new CoreIdleState(modeStateContext, enemy);
            case 1:
                return new CoreAct1State(modeStateContext,enemy);
            case 2:
                return new CoreAct2State(modeStateContext,enemy);
            case 3:
                return new CoreAct3State(modeStateContext, enemy);
            case 4:
                return new CoreDeathState(modeStateContext, enemy);

        }

        return null;
    }
}
