using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CoreAct1State : ModeStateBase
{
    public CoreAct1State(IStateMachine _stateMachine, CoreEnemy enemy) : base(_stateMachine)
    {
        _enemy = enemy;
    }
    private CoreEnemy _enemy;

    public override ModeStateType StateType => throw new System.NotImplementedException();

    public override void OnEnter()
    {
        base.OnEnter();
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

    private async UniTask Attack()
    {
        float _bulletCount = 0;
        while (!_enemy.Token.IsCancellationRequested)
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
                , cancellationToken:_enemy.Token);

        }
    }
}
