using LitMotion;
using System;
using UnityEngine;
using UnityEngine.UI;
public class SettingView : ViewBase
{
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Text closeSettingText;
    [SerializeField]
    private int beforeIndex;

    [SerializeField]
    private Image[] sliderImages;

    [SerializeField]
    private Slider[] sliders;

    //[SerializeField]
    //private Image sliderHandle;

    //[SerializeField]
    //private Button button;

    //[SerializeField]
    //Color unselectColor = Color.gray;
    //[SerializeField]
    //Color selectColor = Color.white;

    [SerializeField]
    private float changeTime = 0.1f;

    [SerializeField]
    private Ease changeEase = Ease.Linear;

    protected override ParamBase GetUseParamBase() => new SettingParam();

    public override void OnInit<T>(T param)
    {
        canvas.gameObject.SetActive(false);
        SettingParam settingParam = param as SettingParam;

        settingParam.nowVolume = volumeSlider.value;
        //volumeSlider.onValueChanged.AddListener((x)=>settingParam.nowVolume = x);
        //volumeSlider.onValueChanged.AddListener(OnChangeValue);
        //volumeSlider.minValue = settingParam.minCameraSensitiveAffinity;
        //volumeSlider.maxValue = settingParam.maxCameraSensitiveAffinity;

        //スライダーの初期化
        sliders[(int)OptionChoiseType.CameraSensitivity].minValue = settingParam.minCameraSensitiveAffinity;
        sliders[(int)OptionChoiseType.CameraSensitivity].maxValue = settingParam.maxCameraSensitiveAffinity;
        sliders[(int)OptionChoiseType.CameraSensitivity].onValueChanged.AddListener(value => settingParam.OnSetCameraSensitivity(value));

        //button.onClick.AddListener(() => volumeSlider.value = 1);

        Image firstSelectSlider = sliderImages[settingParam.currentIndex];
        firstSelectSlider.color = Color.white;
        beforeIndex = settingParam.currentIndex;

    }

    public override void OnAnimation<T>(T param)
    {
        var settingParam = param as SettingParam;
       
        sliderImages[beforeIndex].color = Color.clear;
        sliderImages[settingParam.currentIndex].color = Color.white;

        beforeIndex = settingParam.currentIndex;
    }

    public override void OnReload<T>(T param)
    {
        var settingParam = param as SettingParam;
        Debug.Log($"view,{settingParam.changeAmount}");
        sliders[settingParam.currentIndex].value += settingParam.changeAmount;
        //Tweens.ImageColorTween(sliderHandle, unselectColor, selectColor, changeTime, changeEase, gameObject);
        //Tweens.ImageColorTween(sliderHandle, selectColor, unselectColor, changeTime, changeEase, gameObject);

    }


    public void OnChangeValue(float value)
    {
        Debug.Log(value);
    }


    public override void OnShow<T>(T param)
    {
        canvas.gameObject.SetActive(true);
    }

    public override void OnHide<T>(T param)
    {
        canvas.gameObject.SetActive(false);
    }

}

public enum OptionChoiseType
{
    CameraSensitivity = 0,
    CameraGumma,
    MasterVolume,
    SEVolume,
    BGMVolume
}