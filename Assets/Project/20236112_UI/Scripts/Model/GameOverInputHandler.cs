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
        var playerActionManager = ServiceLocator<PlayerActionManager>.GetInstance();
        _playerAction = playerActionManager.playerAction;

        SetReStartInput();
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
        if (_playerAction != null)
        {
            _onGameOverSubmit?.Invoke();
        }
    }

    public void OnGameOverSelectX(InputAction.CallbackContext context)     //ゲーム終了時の選択入力
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) { return; }
        float directionX = Mathf.Sign(input.x);
        _onGameOverChoice?.Invoke(directionX);
    }


    /// <summary>
    /// リスタート
    /// </summary>
    public void SetReStartInput()
    {
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);
        SetGameOverInput(true);
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
    /// タイトルに移動
    /// </summary>
    public void SetOptionInputEnable(bool isEnable)
    {
        if (isEnable)
        {
            _playerAction.Option.Enable();
        }
        else
        {
            _playerAction.Option.Disable();
        }
    }

    public void SetMenuInputEnable(bool isEnable)
    {
        if (isEnable)
        {
            _playerAction.Menu.Enable();
        }
        else
        {
            _playerAction.Menu.Disable();
        }
    }

    public void SetQuitInputEnable(bool isEnable)
    {
        if (isEnable)
        {
            _playerAction.Quit.Enable();
        }
        else
        {
            _playerAction.Quit.Disable();
        }
    }

    public void SetTitleInput()
    {
        SetTitleInputEnable(true);
        SetGameOverInput(false);
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);

    }

    public void SetPlayerInput()
    {
        SetPlayerInputEnable(true);
        SetGameOverInput(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);
        Time.timeScale = 1.0f;
    }

    public void SetMenuInput()
    {
        SetPlayerInputEnable(false);
        SetMenuInputEnable(true);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);
        Time.timeScale = 0f;
    }

    public void SetOptionInput()
    {
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(true);
        SetQuitInputEnable(false);
    }

    public void SetQuitInput()
    {
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(true);
    }

}
