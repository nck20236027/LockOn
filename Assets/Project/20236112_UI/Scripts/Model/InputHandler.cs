using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputHandler
{
    private PlayerAction _playerAction;

    public Action onMove;
    public Action onMenuAction;
    public Action onSubmit;
    public Action onQuiter;
    public Action onOptionClose;
    //public Action onCancel;

    public Action<float> onSliderSelect;
    public Action<float> onChangeSliderValue;
    public Action<float> onChoice;


    private bool isMenuOpen = false;

    public Button[] buttons;



    public void Init()
    {
        _playerAction = new PlayerAction();
        _playerAction.Player.Enable();
        _playerAction.Player.Menu.canceled += OnMenu; //‚±‚±‚Å“o˜^
        _playerAction.Menu.Navigate.performed += OnUISelectY;
        _playerAction.Menu.Submit.canceled += OnSubmit;
        _playerAction.Option.OptionClose.canceled += OnOptionClose;

        _playerAction.Option.SliderSelected.performed += OnSliderSelect;
        _playerAction.Option.SliderValueChange.performed += OnChangeSliderValue;
        //_playerAction.UI.Navigate.Disable();
    }

    //public void Dispose(InputAction.CallbackContext context)
    //{
    //    _playerAction.UI.Navigate.performed -= OnUISelectY;

    //}



    public void OnUISelectY(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y == 0) { return; }
        float directionY = Mathf.Sign(input.y);
        onChoice(directionY);
    }

    public void OnUISelectX(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) { return; }
        float directionX = Mathf.Sign(input.x);
        onChoice(directionX);
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        SetMenuCloseInput();
        onSubmit();
    }
    public void OnOptionClose(InputAction.CallbackContext context)
    {
        onOptionClose();
    }


    public void OnSliderSelect(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y == 0) { return; }
        float directionY = Mathf.Sign(input.y);
        onSliderSelect(directionY);
        
    }

    public void OnChangeSliderValue(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) { return; }
        float directionX = Mathf.Sign(input.x);
        onChangeSliderValue(directionX);
    }

    public void SetOptionInputEnable(bool isEnable)
    {
        if (isEnable == true)
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
        if (isEnable == true)
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
        if (isEnable == true)
        {
            _playerAction.Player.Enable();
        }
        else
        {
            _playerAction.Player.Disable();
        }
    }

    public void SetOptionOpenInput()
    {
        SetOptionInputEnable(true);
        SetMenuInputEnable(false);
        SetPlayerInputEnable(false);
    }

    public void SetMenuCloseInput()
    {
        SetOptionInputEnable(false);
        SetMenuInputEnable(false);
        SetPlayerInputEnable(true);
    }


    //InputSystem‘¤‚É“o˜^‚·‚é‚â‚Â
    public void OnMenu(InputAction.CallbackContext context)
    {
        onMenuAction(); //MenuHandler‘¤‹@”\
        SetMenuInputEnable(true);
    }


    //public void OnOption(InputAction.CallbackContext context)
    //{
    //    _playerAction.Player.Enable();
    //    _playerAction.UI.Disable();
    //    Debug.Log("UI");
    //}

    //private void OnEnable()
    //{
    //    _playerAction.Enable();
    //}

    //private void OnDisable()
    //{
    //    _playerAction.Disable();
    //}


#if null‹–—eŒ^
    //private Action hogeAction;
    //private void Hoge()
    //{
    //    hogeAction?.Invoke();

    //    if(hogeAction != null)
    //    {
    //        hogeAction();
    //    }
    //}
#endif
}
