using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

public class MenuHandler : MonoBehaviour
{
    private List<IMenuAction> mActions = new List<IMenuAction>();

    //private Dictionary<int ,IMenuAction> mMenuDic;

    private IHasCameraSensitivity hasCameraSensitivity;

    private InputHandler inputHandler = new();

    //menuParam.resameButton = null;
    private PauseParam pauseParam = new();
    private QuitParam quitParam = new();
    private SettingParam settingParam = new();


    private PauseController pauseController = new();
    private QuitController quitController = new();
    private SettingController settingController = new();

    private GameQuiter gameQuiter = new GameQuiter();

    private MenuClose menuClose;
    private OptionClose optionClose;
    private QuitClose quitClose;

    private int _currentIndex = 0;
    private int _optionCurrentIndex = 0;

    private float cameraSensitivity = 0;

    [SerializeField, Range(0, 10)]
    private int changeAmount;
    private float ChangeAmount => changeAmount * 0.1f;
    //private float ChangeAmount { get { return changeAmount * 0.1f; } }

    private void Awake()
    {
        mActions.Add(new MenuClose(pauseParam,inputHandler.SetMenuCloseInput));
        mActions.Add(new OptionOpen(settingParam, pauseParam, inputHandler.SetOptionOpenInput));
        mActions.Add(new QuitOpen(pauseParam,quitParam,inputHandler.SetQuitOpenInput));

        menuClose = new MenuClose(pauseParam,inputHandler.SetMenuCloseInput);
        optionClose = new OptionClose(pauseParam, settingParam, inputHandler.SetOptionInputEnable, inputHandler.SetMenuInputEnable);
        quitClose = new QuitClose(pauseParam, quitParam, inputHandler.SetQuitInputEnable, inputHandler.SetMenuInputEnable);

        //pauseParam.resameButton = pauseController.Resame;
        //pauseParam.quitButton = quitController.OpenQuit;
        //pauseParam.settingButton = settingController.OpenSetting;

        //quitParam.endGame = gameQuiter.QuitGame;
        //quitParam.cancelQuitButton = quitController.CloseQuit;

        //settingParam.closeSettingButton = settingController.CloseSettingMenu;

        //inputHandler.onMenuAction = menuHandler.ControlMenu;  //多和田側で作る時にこんな風に書く
        inputHandler.onMenuAction = ControlMenu;
        inputHandler.onSubmit = OnSubmit;
        inputHandler.onSliderSelect = SliderSelect;
        inputHandler.onChoice = Choice;
        inputHandler.onChangeSliderValue = OnChangeSliderValue;
        inputHandler.onOptionClose = optionClose.CloseOptionAction;
        inputHandler.onQuitClose = quitClose.OnTitleAction;

        inputHandler.Init();
        //mHandler.onAction = mHandler.OnMenu;
    }

    private void Start()
    {
        //menuParam.resameButton = null;    Initより後に処理するとNullのまま生成することになる

        UIMediator.Instance.Init(pauseParam);
        UIMediator.Instance.Init(quitParam);

        //
        UIMediator.Instance.Init(settingParam);

        //MenuParam.settingButton = pauseModel.SettingMenu;
        //MenuParam.resameButton = pauseModel.Resame;

    }


    public void OnChangeCameraSensitivity(float value)
    {

        hasCameraSensitivity.CameraSensitivity = value;
        Debug.Log(cameraSensitivity);
    }

    public void ControlMenu()
    {
        //Menuを開いたり閉じたりする
        UIMediator.Instance.Show(pauseParam);
        inputHandler.SetMenuCloseInput();
        //    UIMediator.Instance.Show(settingParam);
        //    UIMediator.Instance.Show(quitParam);
    }

    public void OnSubmit()
    {
        mActions[_currentIndex].OnMenuAction();
    }

    public void OnChangeSliderValue(float direction)
    {
        Debug.Log(direction);
        settingParam.currentIndex = _optionCurrentIndex;
        settingParam.changeAmount = ChangeAmount * direction;
        Debug.Log($"model,{settingParam.changeAmount}");
        UIMediator.Instance.Reload(settingParam);
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




    public void Choice(float direction)
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


    public interface IHasCameraSensitivity
    {
        public float CameraSensitivity { set; }
    }


    private void Update()
    {


        // ()仮想関数 簡単に言うと()だけで関数を作ってくれる
        //mHandler.onAction(() => {Debug.Log("Hallo World")})   //下の処理がラムダ式だとこれで済む

        //mHandler.onAction = DisplayMessage;
    }

    //private void DisplayMessage()
    //{
    //    Debug.Log("Hello World");
    //}



}

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
