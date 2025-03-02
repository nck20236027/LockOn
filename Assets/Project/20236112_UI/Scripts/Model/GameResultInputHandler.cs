using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameResultInputHandler
{
    private PlayerAction _playerAction;

    public Action _onGameResultSubmit;

    public void Init()
    {
        _playerAction = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        _playerAction.Result.Enable();
        _playerAction.Result.Submit.performed += OnResultSubmit;
    }

    public void Final()
    {
        _playerAction.Result.Submit.performed -= OnResultSubmit;
    }


    public void OnResultSubmit(InputAction.CallbackContext context)        //ゲーム終了の決定
    {
        _onGameResultSubmit?.Invoke();
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
