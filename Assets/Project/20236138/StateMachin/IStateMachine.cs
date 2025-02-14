using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    public ModeStateBase CurrentState { get; }

    public void Initialize(ModeStateType initStateType);

    public void ChangeState(ModeStateType newStateType);

    public void OnEnter();

    //�X�e�[�g�Ǘ����\�b�h
    public void OnUpdate();

    //�X�e�[�g�Ǘ����\�b�h
    public void OnFixedUpdate();

    public void OnExit();

}
