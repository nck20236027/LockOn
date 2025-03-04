using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameResultInputHandler
{
    private PlayerAction _playerAction;

    public Action onGameResultSubmit;

    public void Init()
    {
        var playerActionManager = ServiceLocator<PlayerActionManager>.GetInstance();
        if (playerActionManager == null)
        {
            Debug.LogError("PlayerActionManager is not registered in ServiceLocator.");
            return;
        }

        _playerAction = playerActionManager.playerAction;
        if (_playerAction == null)
        {
            Debug.LogError("PlayerAction is not initialized in PlayerActionManager.");
            return;
        }

        SetResultInput();
        _playerAction.Result.Submit.performed += OnResultSubmit;
    }

    public void Final()
    {
        if (_playerAction != null)
        {
            _playerAction.Result.Submit.performed -= OnResultSubmit;
        }
    }

    public void OnResultSubmit(InputAction.CallbackContext context)
    {
        onGameResultSubmit?.Invoke();
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

    public void SetGameOverInputEnable(bool isEnable)
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
    public void SetResultInputEnable(bool isEnable)
    {
        if (isEnable)
        {
            _playerAction.Result.Enable();
        }
        else
        {
            _playerAction.Result.Disable();
        }
    }

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

    public void SetGameOverInput()
    {
        SetGameOverInputEnable(true);
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);
    }

    public void SetResultInput()
    {
        SetResultInputEnable(true);
        SetGameOverInputEnable(false);
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);
    }

    public void SetTitleInput()
    {
        SetTitleInputEnable(true);
        SetGameOverInputEnable(false);
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);

    }

    public void SetPlayerInput()
    {
        SetPlayerInputEnable(true);
        SetGameOverInputEnable(false);
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
