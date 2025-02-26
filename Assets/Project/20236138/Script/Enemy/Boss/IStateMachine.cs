using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMadeStateMachine
{
    public EnemyModeStateBase CurrentState { get; }

    public void Initialize(int initStateType);

    public void ChangeState(int newStateType);

    public void OnEnter();

    //ステート管理メソッド
    public void OnUpdate();

    //ステート管理メソッド
    public void OnFixedUpdate();

    public void OnExit();

}
