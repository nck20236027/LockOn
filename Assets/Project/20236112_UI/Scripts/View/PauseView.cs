using System;
using UnityEngine;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;


public class PauseView : ViewBase
{
    //[SerializeField]
    //private Button resameButton;
    //[SerializeField]
    //private Button settingButton;
    //[SerializeField]
    //private Button quitButton;

    [SerializeField]
    private Text[] texts;
    [SerializeField]
    private Color tergetColor;

    [SerializeField] 
    private RectTransform[] rectTransforms;

    [SerializeField] 
    private Vector3 tweenSale;

    //[SerializeField]
    private int tergetFontSize = 100;
    private int initFointSize = 60;

    private int beforeIndex;

    [SerializeField]
    private Ease changeEase = Ease.Linear;

    [SerializeField]
    float expandTime = 0.1f;
    [SerializeField]
    float reduceTime = 0.1f;


    [SerializeField]
    Color unselectColor = Color.black;
    [SerializeField]
    Color selectColor = Color.white;


    protected override ParamBase GetUseParamBase() => new PauseParam();

    public override void OnInit<T>(T param)
    {
        canvas.gameObject.SetActive(false);   //生成時にどう表示するか
        PauseParam pauseParam = param as PauseParam;

        Text firstSelectText = texts[pauseParam.currentIndex];

        Tweens.FontSizeTween(firstSelectText, initFointSize, tergetFontSize, expandTime, changeEase, gameObject);
        Tweens.TextColorTween(firstSelectText, tergetColor, tergetColor, expandTime, changeEase, gameObject);


        beforeIndex = pauseParam.currentIndex;

        //base.OnInit<T>(param);                もしスケールが０になってたらこの処理を書く
        //resameButton.onClick.AddListener(() => pauseParam.resameButton());
        //settingButton.onClick.AddListener(() => pauseParam.settingButton());
        //quitButton.onClick.AddListener(() => pauseParam.quitButton());
    }


    public override void OnReload<T>(T param)
    {
        var pauseParam = param as PauseParam;
        Text tweenText = texts[pauseParam.currentIndex];
        Text beforeText = texts[beforeIndex];

        RectTransform rectTransform = rectTransforms[pauseParam.currentIndex];

        Tweens.TextColorTween(beforeText, tergetColor, unselectColor, expandTime, changeEase, gameObject);
        Tweens.FontSizeTween(beforeText, tergetFontSize, initFointSize, reduceTime, changeEase, gameObject);
     
        Tweens.TextColorTween(tweenText, tergetColor, selectColor, expandTime, changeEase, gameObject);
        Tweens.FontSizeTween(tweenText, initFointSize, tergetFontSize, expandTime, changeEase, gameObject);

        beforeIndex = pauseParam.currentIndex;
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
