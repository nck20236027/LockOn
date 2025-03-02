using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreIdleState : EnemyModeStateBase
{
    CoreEnemy _coreEnemy;
    public CoreIdleState(IMadeStateMachine _stateMachine,CoreEnemy enemy) : base(_stateMachine)
    {
        _enemy = enemy;
    }
    private CoreEnemy _enemy;

    public override ModeStateType StateType => throw new System.NotImplementedException();

    public async override void OnEnter()
    {
        base.OnEnter();
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.ActionIntarval));
        stateMachine.ChangeState(UnityEngine.Random.Range(1,4));
        ServiceLocator<SEManager>.GetInstance().PlaySoundToPan( _coreEnemy, _coreEnemy.coreStateSound,true);
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
