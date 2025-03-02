using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameOverInputHandler
{

    private PlayerAction _playerAction;

    public Action _onGameOverSubmit;
    public Action<float> _onGameOverChoice;

    public void Init()
    {
        _playerAction = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        _playerAction.GameOver.Enable();
        _playerAction.GameOver.Submit.performed += OnGameOverSubmit;
        _playerAction.GameOver.Navigate.performed += OnGameOverSelectX;
    }

    public void Final()
    {
        _playerAction.GameOver.Submit.performed -= OnGameOverSubmit;
        _playerAction.GameOver.Navigate.performed -= OnGameOverSelectX;
    }

    public void OnGameOverSubmit(InputAction.CallbackContext context)        //ゲーム終了の決定
    {
        _onGameOverSubmit?.Invoke();
    }

    public void OnGameOverSelectX(InputAction.CallbackContext context)     //ゲーム終了時の選択入力
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) { return; }
        float directionX = Mathf.Sign(input.x);
        _onGameOverChoice?.Invoke(directionX);
    }

    public void SetPlayerInputEnable(bool isEnable)
    {
        if (isEnable)
        {
            _playerAction.Player.Enable();
        }
        else
        {
            _playerAction.Player.Disable();
        }
    }

    public void SetTitleInputEnable(bool isEnable)
    {
        if (isEnable)
        {
            _playerAction.Title.Enable();
        }
        else
        {
            _playerAction.Title.Disable();
        }
    }

    public void SetGameOverInput(bool isEnable)
    {
        if (isEnable)
        {
            _playerAction.GameOver.Enable();
        }
        else
        {
            _playerAction.GameOver.Disable();
        }
    }

    /// <summary>
    /// リスタート
    /// </summary>
    public void SetReStartInput()
    {
        SetPlayerInputEnable(true);
        SetGameOverInput(false);
    }

    /// <summary>
    /// タイトルに移動
    /// </summary>
    public void SetTitleInput()
    {
        SetTitleInputEnable(true);
        SetGameOverInput(false);

    }
}
