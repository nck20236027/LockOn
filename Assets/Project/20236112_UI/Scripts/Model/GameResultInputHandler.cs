using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameResultInputHandler : MonoBehaviour
{
    private PlayerAction _playerActions;
    public Action _onResultSubmit;
    //public Action<float> _onGameEndChoice;

    public void Init()
    {
        _playerActions = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        _playerActions.Result.Submit.performed += OnResultSubmit;
        //_playerActions.GameEnd.Navigate.performed += OnGameEndSelectX;
    }
    public void Final()
    {
        _playerActions.Result.Submit.performed -= OnResultSubmit;
        //_playerActions.GameEnd.Navigate.performed -= OnGameEndSelectX;
    }
    public void OnResultSubmit(InputAction.CallbackContext context)        //ゲーム終了の決定
    {
        _onResultSubmit();
    }
    //public void OnGameEndSelectX(UnityEngine.InputSystem.InputAction.CallbackContext context)     //ゲーム終了時の選択入力
    //{
    //    Vector2 input = context.ReadValue<Vector2>();
    //    if (input.x == 0) { return; }
    //    float directionX = Mathf.Sign(input.x);
    //    _onGameEndChoice(directionX);
    //}
}
