using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleInputHandler
{
    private PlayerAction _inputActions;

    public Action _onStartSubmit;
    public Action _onEndSubmit;
    public Action<float> _onTitleChoice;
    public Action<float> _onGameEndChoice;


    public void Init()
    {
        _inputActions = new();
        _inputActions.Title.Submit.canceled += OnStartSubmit;
        _inputActions.Title.Navigate.performed += OnTitleSelectY;

        _inputActions.GameEnd.Submit.canceled += OnEndSubmit;
        _inputActions.GameEnd.Navigate.performed += OnGameEndSelectX;
    }


    public void OnStartSubmit(InputAction.CallbackContext context)      //スタート時の決定
    {
        _onStartSubmit();
    }

    public void OnEndSubmit(InputAction.CallbackContext context)        //ゲーム終了の決定
    {
        _onEndSubmit();
    }

    public void OnTitleSelectY(InputAction.CallbackContext context)     //タイトル画面の選択入力
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y == 0) { return; }
        float directionY = Mathf.Sign(input.y);
        _onTitleChoice(directionY);
    }

    public void OnGameEndSelectX(InputAction.CallbackContext context)     //ゲーム終了時の選択入力
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) { return; }
        float directionX = Mathf.Sign(input.x);
        _onGameEndChoice(directionX);
    }

    public void OnPlayerInputEneble(bool isEneble)      //ActionMapのプレイヤー切り替え
    {
        if (isEneble)
        {
            _inputActions.Player.Enable();
        }
        else
        {
            _inputActions.Player.Disable();
        }
    }
    public void OnGameEndInputEneble(bool isEneble)     //ActionMapのGameEndの切り替え
    {
        if (isEneble)
        {
            _inputActions.GameEnd.Enable();
        }
        else
        {
            _inputActions.GameEnd.Disable();
        }
    }

    public void OnTitleInputEneble(bool isEneble)
    {
        if (isEneble)
        {
            _inputActions.Title.Enable();
        }
        else
        {
            _inputActions.Title.Disable();
        }
    }

    public void SetGameStart()      //ゲーム開始
    {
        OnPlayerInputEneble(true);
        OnGameEndInputEneble(false);
    }

    public void SetGameEnd()        //ゲーム終了
    {
        OnPlayerInputEneble(false);
        OnGameEndInputEneble(true);
    }
}
