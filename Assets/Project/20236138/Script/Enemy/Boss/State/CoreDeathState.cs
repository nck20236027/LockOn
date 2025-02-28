using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDeathState : EnemyModeStateBase
{
    CoreEnemy _enemy;
    public CoreDeathState(IMadeStateMachine _stateMachine,CoreEnemy _enemy) : base(_stateMachine)
    {
        this._enemy = _enemy;
    }

    public override ModeStateType StateType => throw new System.NotImplementedException();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
