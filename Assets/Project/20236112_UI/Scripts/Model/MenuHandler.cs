using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    private List<IMenuAction> menuActions = new List<IMenuAction>();
    private List<IQuitAction> quitActions = new List<IQuitAction>();

    //private Dictionary<int ,IMenuAction> mMenuDic;

    private IHasCameraSensitivity hasCameraSensitivity;

    private InputHandler inputHandler = new();

    //menuParam.resameButton = null;
    private PauseParam pauseParam = new();
    private QuitParam quitParam = new();
    private SettingParam settingParam = new();


    //private PauseController pauseController = new();
    //private QuitController quitController = new();
    //private SettingController settingController = new();

    private GameQuiter gameQuiter = new GameQuiter();

    private MenuClose menuClose;
    private OptionClose optionClose;
    private QuitClose quitClose;

    private int _currentIndex = 0;
    private int _optionCurrentIndex = 0;
    private int _quitCurrentIndex = 0;

    private float cameraSensitivity = 0;

    [SerializeField]
    private float[] changeAmoutValues;

    //private float ChangeAmount { get { return changeAmount * 0.1f; } }

    private void Awake()
    {
        menuActions.Add(new MenuClose(pauseParam, inputHandler.SetMenuInput));
        menuActions.Add(new OptionOpen(settingParam, pauseParam, inputHandler.SetOptionInput));
        menuActions.Add(new QuitOpen(pauseParam, quitParam, inputHandler.SetQuitInput));

        quitActions.Add(new QuitClose(pauseParam, quitParam, inputHandler.SetMenuInput));
        quitActions.Add(new GameQuiter());

        menuClose = new MenuClose(pauseParam, inputHandler.SetPlayerInput);
        optionClose = new OptionClose(pauseParam, settingParam, inputHandler.SetMenuInput);
        quitClose = new QuitClose(pauseParam, quitParam, inputHandler.SetQuitInput);

        //pauseParam.resameButton = pauseController.Resame;
        //pauseParam.quitButton = quitController.OpenQuit;
        //pauseParam.settingButton = settingController.OpenSetting;

        //quitParam.endGame = gameQuiter.QuitGame;
        //quitParam.cancelQuitButton = quitController.CloseQuit;

        //settingParam.closeSettingButton = settingController.CloseSettingMenu;

        //inputHandler.onMenuAction = menuHandler.ControlMenu;  //多和田側で作る時にこんな風に書く
        inputHandler.onMenuAction = ControlMenu;
        inputHandler.onMenuSubmit = OnMenuSubmit;
        inputHandler.onQuitSubmit = OnQuitSubmit;

        inputHandler.onSliderSelect = SliderSelect;
        inputHandler.onMenuChoice = MenuChoice;
        inputHandler.onQuitChoice = QuitChoice;
        inputHandler.onChangeSliderValue = OnChangeSliderValue;

        inputHandler.onOptionClose = optionClose.CloseOptionAction;
        inputHandler.onQuitClose = quitClose.OnQuitAction;

        inputHandler.Init();
        //mHandler.onAction = mHandler.OnMenu;
    }


    //サービスロケーターで受け取る
    private void Start()
    {
        //menuParam.resameButton = null;    Initより後に処理するとNullのまま生成することになる

        UIMediator.Instance.Init(pauseParam);
        UIMediator.Instance.Init(quitParam);

        //
        settingParam.onSetCameraSensitivity = OnChangeCameraSensitivity;
        settingParam.onChangeSEVolue = OnChangeSEVolume;
        settingParam.minSEVolue = ServiceLocator<SEManager>.GetInstance().MinVolumeValue;
        settingParam.maxSEVolue = ServiceLocator<SEManager>.GetInstance().MaxVolumeValue;

        settingParam.minCameraSensitiveAffinity = ServiceLocator<CameraController>.GetInstance().MinIntensity;
        settingParam.maxCameraSensitiveAffinity = ServiceLocator<CameraController>.GetInstance().MaxIntensity;

        settingParam.initCameraSensitiveAffinity = ServiceLocator<CameraController>.GetInstance().BasisIntensity;
        UIMediator.Instance.Init(settingParam);

        //MenuParam.settingButton = pauseModel.SettingMenu;
        //MenuParam.resameButton = pauseModel.Resame;

    }

    //ここも
    public void OnChangeCameraSensitivity(float value)
    {

        ServiceLocator<CameraController>.GetInstance().BasisIntensity = value;
        Debug.Log(cameraSensitivity);
    }

    public void ControlMenu()
    {
        //Menuを開いたり閉じたりする
        UIMediator.Instance.Show(pauseParam);
        inputHandler.SetPlayerInput();
        //    UIMediator.Instance.Show(settingParam);
        //    UIMediator.Instance.Show(quitParam);
    }

    public void OnMenuSubmit()
    {
        Debug.Log("MenuSubmit");

        menuActions[_currentIndex].OnMenuAction();
    }

    public void OnQuitSubmit()
    {
        Debug.Log("QuitSubmit");
        quitActions[_quitCurrentIndex].OnQuitAction();
    }

    public void OnChangeSliderValue(float direction)
    {
        Debug.Log(direction);
        settingParam.currentIndex = _optionCurrentIndex;
        settingParam.changeAmount = changeAmoutValues[_currentIndex] * direction;
        Debug.Log($"model,{settingParam.changeAmount}");
        UIMediator.Instance.Reload(settingParam);
    }

    public void OnChangeSEVolume(float value)
    {
        ServiceLocator<SEManager>.GetInstance().SetSEVolume(value);
    }


    public void SliderSelect(float direction)
    {
        _optionCurrentIndex -= (int)direction;
        if (_optionCurrentIndex > 1)
        {
            _optionCurrentIndex = 1;
            return;
        }
        if (_optionCurrentIndex < 0)
        {
            _optionCurrentIndex = 0;
            return;
        }
        settingParam.currentIndex = _optionCurrentIndex;
        UIMediator.Instance.Animation(settingParam);
    }

    public void MenuChoice(float direction)
    {
        _currentIndex -= (int)direction;
        if (_currentIndex > 2)
        {
            _currentIndex = 2;
            return;
        }
        if (_currentIndex < 0)
        {
            _currentIndex = 0;
            return;
        }

        pauseParam.currentIndex = _currentIndex;
        UIMediator.Instance.Reload(pauseParam);
    }

    public void QuitChoice(float direction)
    {
        //Debug.Log(_quitCurrentIndex);
        _quitCurrentIndex -= (int)direction;
        if (_quitCurrentIndex > 1)
        {
            _quitCurrentIndex = 1;
            return;
        }
        if (_quitCurrentIndex < 0)
        {
            _quitCurrentIndex = 0;
            return;
        }
        quitParam.currentIndex = _quitCurrentIndex;
        UIMediator.Instance.Reload(quitParam);
    }


    public interface IHasCameraSensitivity
    {
        public float CameraSensitivity { set; }
    }


    private void OnDestroy()
    {
        UIMediator.Instance.Final(pauseParam);
        UIMediator.Instance.Final(settingParam);
        UIMediator.Instance.Final(quitParam);
        inputHandler.Final();
    }
}
    ////private void DisplayMessage()
    //{
    //    Debug.Log("Hello World");
    //}




//public class Player
//{
//    private PlayerAction playerAction;

//    public void Start()
//    {

//        playerAction = new PlayerAction();
//    }
//}

//public class Menu
//{
//    private PlayerAction playerAction;

//    public void Start()
//    {
//        playerAction = new PlayerAction();
//    }
//}
