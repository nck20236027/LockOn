using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    public ModeStateBase CurrentState { get; }

    public void Initialize(ModeStateType initStateType);

    public void ChangeState(ModeStateType newStateType);

    public void OnEnter();

    //ステート管理メソッド
    public void OnUpdate();

    //ステート管理メソッド
    public void OnFixedUpdate();

    public void OnExit();

}
