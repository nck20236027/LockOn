using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DeathState : ModeStateBase
{
    Player _player;
    public DeathState(IStateMachine _stateMachine,Player _player) : base(_stateMachine)
    {
        this._player = _player;
    }

    public override ModeStateType StateType => throw new System.NotImplementedException();

    public override async void OnEnter()
    {
        base.OnEnter();
        _player._cameraController.Interface.CameraStop();
        _player.GetRigidbody.isKinematic = false;
        _player.GetRigidbody.useGravity = true;

        await UniTask.Delay(TimeSpan.FromSeconds(_player.StandbyTime),cancellationToken:_player.destroyCancellationToken);
        ServiceLocator<UIMediator>.GetInstance().Init(_player._gameOverParam);
        ServiceLocator<UIMediator>.GetInstance().Show(_player._gameOverParam);
        ServiceLocator<UIMediator>.GetInstance().Reload(_player._gameOverParam);
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
