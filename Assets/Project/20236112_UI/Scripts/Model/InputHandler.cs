using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputHandler
{
    private PlayerAction _playerAction;

    public Action onMove;
    public Action onMenuAction;
    public Action onMenuSubmit;
    public Action onQuitSubmit;
    public Action onOptionClose;
    public Action onQuitClose;

    public Action<float> onSliderSelect;
    public Action<float> onChangeSliderValue;
    public Action<float> onMenuChoice;
    public Action<float> onQuitChoice;

    public void Init()
    {
        _playerAction = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        _playerAction.Player.Enable();

        SetPlayerInput();

        _playerAction.Player.Menu.performed += OnMenu;
        _playerAction.Menu.Navigate.performed += OnUISelectY;
        _playerAction.Menu.Submit.canceled += OnSubmit;
        _playerAction.Quit.Submit.canceled += OnQuitSubmit;
        _playerAction.Quit.Navigate.performed += OnUISelectX;
        _playerAction.Option.OptionClose.canceled += OnOptionClose;
        _playerAction.Option.SliderSelected.performed += OnSliderSelect;
        _playerAction.Option.SliderValueChange.performed += OnChangeSliderValueX;
    }

    public void Final()
    {
        _playerAction.Player.Menu.performed -= OnMenu;
        _playerAction.Menu.Navigate.performed -= OnUISelectY;
        _playerAction.Menu.Submit.canceled -= OnSubmit;
        _playerAction.Quit.Submit.canceled -= OnQuitSubmit;
        _playerAction.Quit.Navigate.performed -= OnUISelectX;
        _playerAction.Option.OptionClose.canceled -= OnOptionClose;
        _playerAction.Option.SliderSelected.performed -= OnSliderSelect;
        _playerAction.Option.SliderValueChange.performed -= OnChangeSliderValueX;

        _playerAction.Menu.Disable();
        _playerAction.Quit.Disable();
        _playerAction.Option.Disable();
        _playerAction.Player.Disable();
    }

    public void OnUISelectY(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y == 0) return;
        float directionY = Mathf.Sign(input.y);
        onMenuChoice?.Invoke(directionY);
    }

    public void OnUISelectX(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) return;
        float directionX = Mathf.Sign(input.x);
        onQuitChoice?.Invoke(directionX);
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        onMenuSubmit?.Invoke();
    }

    public void OnQuitSubmit(InputAction.CallbackContext context)
    {
        onQuitSubmit?.Invoke();
    }

    public void OnOptionClose(InputAction.CallbackContext context)
    {
        onOptionClose?.Invoke();
    }

    public void OnSliderSelect(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y == 0) return;
        float directionY = Mathf.Sign(input.y);
        onSliderSelect?.Invoke(directionY);
    }

    public void OnChangeSliderValueX(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) return;
        float directionX = Mathf.Sign(input.x);
        onChangeSliderValue?.Invoke(directionX);
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

    public void SetPlayerInput()
    {
        SetPlayerInputEnable(true);
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

    public void OnMenu(InputAction.CallbackContext context)
    {
        //Debug.Log("OpenMenu");
        onMenuAction?.Invoke();
        SetMenuInput();
    }

    public void OnGameQuit(InputAction.CallbackContext context)
    {
        onQuitSubmit?.Invoke();
    }
}
