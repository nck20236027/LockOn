using System;
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
    //public Action onCancel;

    public Action<float> onSliderSelect;
    public Action<float> onChangeSliderValue;
    public Action<float> onMenuChoice;
    public Action<float> onQuitChoice;


    //private bool isMenuOpen = false;

    //public Button[] buttons;



    public void Init()
    {
        _playerAction = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;

        SetPlayerInput();

        _playerAction.Player.Menu.canceled += OnMenu; //Ç±Ç±Ç≈ìoò^

        _playerAction.Menu.Navigate.performed += OnUISelectY;
        _playerAction.Menu.Submit.canceled += OnSubmit;

        _playerAction.Quit.Submit.canceled += OnQuitSubmit;
        _playerAction.Quit.Navigate.performed += OnUISelectX;

        _playerAction.Option.OptionClose.canceled += OnOptionClose;
        _playerAction.Option.SliderSelected.performed += OnSliderSelect;
        _playerAction.Option.SliderValueChange.performed += OnChangeSliderValueX;
        //_playerAction.UI.Navigate.Disable();
    }

    public void Final()
    {
        _playerAction.Dispose();
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
        onMenuChoice(directionY);
    }

    public void OnUISelectX(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) { return; }
        float directionX = Mathf.Sign(input.x);
        onQuitChoice(directionX);
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        //SetMenuInput();
        onMenuSubmit();
    }

    public void OnQuitSubmit(InputAction.CallbackContext context)
    {
        onQuitSubmit();
    }

    public void OnOptionClose(InputAction.CallbackContext context)
    {
        onOptionClose();
    }

    //public void OnQuitCancel(InputAction.CallbackContext context)
    //{
    //    SetQuitCloseInput();
    //    onQuitClose();
    //}

    public void OnSliderSelect(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y == 0) { return; }
        float directionY = Mathf.Sign(input.y);
        onSliderSelect(directionY);

    }

    public void OnChangeSliderValueX(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) { return; }
        float directionX = Mathf.Sign(input.x);
        onChangeSliderValue(directionX);
    }
    public void OnChangeSliderValueY(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y == 0) { return; }
        float directionX = Mathf.Sign(input.y);
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
    public void SetQuitInputEnable(bool isEnable)
    {
        if (isEnable == true)
        {
            _playerAction.Quit.Enable();
        }
        else
        {
            _playerAction.Quit.Disable();
        }
    }

    public void SetPlayerInput()            //ÉvÉåÉCÉÑÅ[ÇæÇØ
    {
        SetPlayerInputEnable(true);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);
        Debug.Log($"SetPlayerInput");
    }

    public void SetMenuInput()             //ÉÅÉjÉÖÅ[ÇæÇØ
    {
        SetPlayerInputEnable(false);
        SetMenuInputEnable(true);
        SetOptionInputEnable(false);
        SetQuitInputEnable(false);
        Debug.Log($"SetMenuInput");
    }

    public void SetOptionInput()
    {
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(true);
        SetQuitInputEnable(false);
        Debug.Log($"SetOptionInput");
    }

    public void SetQuitInput()
    {
        SetPlayerInputEnable(false);
        SetMenuInputEnable(false);
        SetOptionInputEnable(false);
        SetQuitInputEnable(true);
        Debug.Log($"SetQuitInput");
    }


    //InputSystemë§Ç…ìoò^Ç∑ÇÈÇ‚Ç¬
    public void OnMenu(InputAction.CallbackContext context)
    {
        onMenuAction(); //MenuHandlerë§ã@î\
        SetMenuInput();
    }

    public void OnGameQuit(InputAction.CallbackContext context)
    {
        onQuitSubmit();
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


#if nullãñóeå^
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
