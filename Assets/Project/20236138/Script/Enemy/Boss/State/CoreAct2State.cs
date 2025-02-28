using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CoreAct2State : EnemyModeStateBase
{
    float _attackTime = 0f;
    public CoreAct2State(IMadeStateMachine _stateMachine, CoreEnemy enemy) : base(_stateMachine)
    {
        _enemy = enemy;
    }
    private CoreEnemy _enemy;

    public override ModeStateType StateType => throw new System.NotImplementedException();

    public async override void OnEnter()
    {
        base.OnEnter();
        _enemy.LineRenderer.SetPosition(0,_enemy.transform.position);
        _enemy.LineRenderer.enabled = false;
        _attackTime = 0f;
        TargetManager.Instance.AddLockTarget(_enemy);
        CancellationTokenSource cancellation = new();
        CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(_enemy.Token, cancellation.Token);

        _enemy.IsAttack = true;
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.AnimationTime),cancellationToken: tokenSource.Token); 
        try
        {

        _ = CreatBullet(tokenSource.Token);
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Act2AttackTime), cancellationToken: _enemy.Token);
        cancellation.Cancel();
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Act2ChanseTime), cancellationToken: _enemy.Token);
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
        _attackTime += Time.fixedDeltaTime;
        if ((_enemy.transform.position - _enemy.GetPlayerPos).sqrMagnitude < Mathf.Pow(_enemy.SreachDistance, 2))
        {
            _enemy.transform.rotation = Quaternion.LookRotation(TargetManager.Instance.GetPlayerPos - _enemy.transform.position, Vector3.up);
            _enemy.LineRenderer.enabled = true;
            _enemy.LineRenderer.SetPosition(1, _enemy.GetPlayerPos - _enemy.transform.position);
        }
        else
        {
            _enemy.LineRenderer.enabled = false;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    private async UniTask CreatBullet(CancellationToken token)
    {
        while(!token.IsCancellationRequested)
        {

            await UniTask.Delay(0, cancellationToken: token);
        if ((_enemy.transform.position - _enemy.GetPlayerPos).sqrMagnitude < Mathf.Pow(_enemy.SreachDistance, 2) &&
                _attackTime <_enemy.EnemyBulletSpan) continue;
        _attackTime = 0;
        for (int j = 0; j < _enemy.Act2BulletCount; j++)
        {
            for (int i = -_enemy.Act2BulletLineCount; i <= _enemy.Act2BulletLineCount; i++)
            {
                Quaternion _rotation =  Quaternion.Euler(0,_enemy.AnemyBulletRotation * i, 0) * _enemy.transform.rotation;
                Vector3 _pos = _rotation * Vector3.forward * _enemy.EnemyBulletInstatiateDistance;
                _enemy.BulletPool.GetBullet(_enemy.transform.position + _pos, _rotation, _enemy.Act2BulletStatus);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.EnemyBulletDistance),cancellationToken:token);
        }
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.EnemyBulletSpan), cancellationToken: token);
        }
    }
}
