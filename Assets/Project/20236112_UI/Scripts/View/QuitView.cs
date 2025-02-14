using LitMotion;
using System;
using UnityEngine;
using UnityEngine.UI;

public class QuitView : ViewBase
{
    //[SerializeField]
    //private Button acceptButton;

    //[SerializeField]
    //private Button quitCancelButton;

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


    //public override Type GetParamType() => typeof(QuitParam);
    protected override ParamBase GetUseParamBase() => new QuitParam();


    public override void OnInit<T>(T param)
    {
        canvas.gameObject.SetActive(false);
        QuitParam quitParam = param as QuitParam;

        //Text firstSelectText = texts[quitParam.currentIndex];

        //Tweens.FontSizeTween(firstSelectText, initFointSize, tergetFontSize, expandTime, changeEase, gameObject);
        //Tweens.TextColorTween(firstSelectText, tergetColor, tergetColor, expandTime, changeEase, gameObject);

        //beforeIndex = quitParam.currentIndex;

        //acceptButton.onClick.AddListener(() => quitParam.endGame());
        //quitCancelButton.onClick.AddListener(()=> quitParam.cancelQuitButton());
    }

    public override void OnReload<T>(T param)
    {
        //var quitParam = param as PauseParam;
        //Text tweenText = texts[quitParam.currentIndex];
        //Text beforeText = texts[beforeIndex];

        //RectTransform rectTransform = rectTransforms[quitParam.currentIndex];

        //Tweens.TextColorTween(beforeText, tergetColor, unselectColor, expandTime, changeEase, gameObject);
        //Tweens.FontSizeTween(beforeText, tergetFontSize, initFointSize, reduceTime, changeEase, gameObject);

        //Tweens.TextColorTween(tweenText, tergetColor, selectColor, expandTime, changeEase, gameObject);
        //Tweens.FontSizeTween(tweenText, initFointSize, tergetFontSize, expandTime, changeEase, gameObject);

        //beforeIndex = quitParam.currentIndex;

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
