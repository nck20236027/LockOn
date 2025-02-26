using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleInputHandler
{
    private PlayerAction _playerActions;

    public Action _onStartSubmit;
    public Action _onEndSubmit;
    public Action<float> _onTitleChoice;
    public Action<float> _onGameEndChoice;


    public void Init()
    {
        _playerActions = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        _playerActions.Title.Enable();
        _playerActions.Title.Submit.canceled += OnStartSubmit;
        _playerActions.Title.Navigate.performed += OnTitleSelectY;

        _playerActions.GameEnd.Submit.canceled += OnEndSubmit;
        _playerActions.GameEnd.Navigate.performed += OnGameEndSelectX;
    }

    public void Final()
    {
        _playerActions.Dispose();
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
            _playerActions.Player.Enable();
        }
        else
        {
            _playerActions.Player.Disable();
        }
    }
    public void OnGameEndInputEneble(bool isEneble)     //ActionMapのGameEndの切り替え
    {
        if (isEneble)
        {
            _playerActions.GameEnd.Enable();
        }
        else
        {
            _playerActions.GameEnd.Disable();
        }
    }

    public void OnTitleInputEneble(bool isEneble)
    {
        if (isEneble)
        {
            _playerActions.Title.Enable();
        }
        else
        {
            _playerActions.Title.Disable();
        }
    }

    public void SetGameStart()      //ゲーム開始
    {
        _playerActions.Player.Enable();
        _playerActions.GameEnd.Disable();
        _playerActions.Player.Enable();
    }

    public void SetGameEnd()        //ゲーム終了
    {
        OnPlayerInputEneble(false);
        OnGameEndInputEneble(true);
        OnTitleInputEneble(false);
    }

    public void SetTitle()
    {
        OnPlayerInputEneble(false);
        OnGameEndInputEneble(false);
        OnTitleInputEneble(true);
    }
}
