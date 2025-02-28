using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CoreAct1State : EnemyModeStateBase
{
    CancellationTokenSource token = new CancellationTokenSource();
    public CoreAct1State(IMadeStateMachine _stateMachine, CoreEnemy enemy) : base(_stateMachine)
    {
        _enemy = enemy;
    }

    ~CoreAct1State()
    {
        token.Cancel();
    }
    private CoreEnemy _enemy;

    public override ModeStateType StateType => throw new System.NotImplementedException();

    public override  async void OnEnter()
    {
        base.OnEnter();
        TargetManager.Instance.AddLockTarget(_enemy);
        CancellationTokenSource cancellation = new();
        CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(_enemy.Token,cancellation.Token);
        _enemy.IsAttack = true;
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.AnimationTime),cancellationToken:tokenSource.Token);
        try
        {
        _ = CreatBullet(tokenSource.Token);

        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Act1AttackTime), cancellationToken: _enemy.Token);
        cancellation.Cancel();
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Act1ChanseTime), cancellationToken: _enemy.Token);
        }
        catch  { 
        }
        stateMachine.ChangeState(0);
    }

    public override void OnExit()
    {
        base.OnExit();
        _enemy.IsAttack = false;
        TargetManager.Instance?.RemoveLockTarget(_enemy);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    private async UniTask CreatBullet(CancellationToken token)
    {
        float _bulletCount = 0;

        while (!token.IsCancellationRequested)
        {
            _bulletCount += 360 * (_enemy.Act1CreatBulletIntarval / _enemy.BulletAround);
            _bulletCount %= 360;
            Quaternion _rotation = _enemy.transform.rotation * Quaternion.Euler(0, _bulletCount, 0);
            Vector3 _vector = _rotation * Vector3.forward;
            EnemyBulletStatus status = _enemy.Act1BulletStatus;
            status._DestroyTime = _enemy.Act1DestroyTime;
            _enemy.BulletPool.GetBullet(_enemy.transform.position + _vector * _enemy.DistanceAttack
                , _rotation, status);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Act1CreatBulletIntarval)
                , cancellationToken:token);

        }
    }


    
}
