using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CoreAct3State : EnemyModeStateBase
{
    public CoreAct3State(IMadeStateMachine _stateMachine, CoreEnemy enemy) : base(_stateMachine)
    {
        _enemy = enemy;
    }
    private CoreEnemy _enemy;

    public override ModeStateType StateType => throw new System.NotImplementedException();

    public async override void OnEnter()
    {
        base.OnEnter();
        TargetManager.Instance.AddLockTarget(_enemy);
        CancellationTokenSource cancellation = new();
        CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(_enemy.Token, cancellation.Token);
        _enemy.IsAttack = true;
        try
        {
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.AnimationTime), cancellationToken: tokenSource.Token);
         _ = CreatTriangleEnemy(tokenSource.Token);

        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Act3AttackTime), cancellationToken: _enemy.Token);
        tokenSource.Cancel();
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Act3ChanseTime), cancellationToken: _enemy.Token);
        }
        catch
        {
 
        }
        stateMachine.ChangeState((int)CoreEnemyState.Idle);
    }

    public override void OnExit()
    {
        base.OnExit();
        TargetManager.Instance.RemoveLockTarget(_enemy);
        _enemy.IsAttack = false;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    private async UniTask CreatTriangleEnemy(CancellationToken _token)
    {
        while(!_token.IsCancellationRequested)
        {
            float _enemyRotarion = 360f * (UnityEngine.Random.Range(0f,10f) / 10);
            _enemyRotarion %= 360;
            Quaternion _rotation = _enemy.transform.rotation * Quaternion.Euler(0, _enemyRotarion, 0);
            Vector3 _vector = _enemy.transform.position + _rotation * Vector3.forward * _enemy.Act3EnemyCreatDistance;
            _enemy.CreatTriangleEnemy(_vector, Quaternion.Euler(0, _enemyRotarion, 0));
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Act3CreatEnemyInterval), cancellationToken: _token);
        }
    }
}
