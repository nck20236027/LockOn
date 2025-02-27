using LitMotion;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : ViewBase
{
    [SerializeField]
    private Text[] _texts;
    private int _beforeIndex;

    [SerializeField]
    private Color _tergetColor;

    [SerializeField]
    private RectTransform[] _rectTransforms;

    private int _tergetFontSize = 100;
    private int _initFointSize = 60;


    [SerializeField]
    private Ease changeEase = Ease.Linear;

    [SerializeField]
    private float _expandTime = 0.1f;
    [SerializeField]
    private float _reduceTime = 0.1f;


    [SerializeField]
    private Color _unselectColor = Color.black;
    [SerializeField]
    private Color _selectColor = Color.white;

    protected override ParamBase GetUseParamBase() => new GameOverParam();
    public override void OnInit<T>(T param)
    {
        gameObject.SetActive(true);
        GameOverParam gameOverParam = param as GameOverParam;

        Text firstSelectText = _texts[gameOverParam.currentIndex];
        Tweens.FontSizeTween(firstSelectText, _initFointSize, _tergetFontSize, _expandTime, changeEase, gameObject);
        Tweens.TextColorTween(firstSelectText, _tergetColor, _tergetColor, _expandTime, changeEase, gameObject);

        _beforeIndex = gameOverParam.currentIndex;
    }

    public override void OnReload<T>(T param)
    {
        var gameOverParam = param as GameOverParam;
        Text tweenText = _texts[gameOverParam.currentIndex];
        Text beforeText = _texts[_beforeIndex];

        RectTransform rectTransform = _rectTransforms[gameOverParam.currentIndex];

        Tweens.FontSizeTween(tweenText, _initFointSize, _tergetFontSize, _expandTime, changeEase, gameObject);
        Tweens.FontSizeTween(beforeText, _tergetFontSize, _initFointSize, _reduceTime, changeEase, gameObject);

        Tweens.TextColorTween(tweenText, _tergetColor, _selectColor, _expandTime, changeEase, gameObject);
        Tweens.TextColorTween(beforeText, _tergetColor, _unselectColor, _reduceTime, changeEase, gameObject);

        _beforeIndex = gameOverParam.currentIndex;
    }

    public override void OnShow<T>(T param)
    {
        canvas.gameObject.SetActive(true);
    }
    public override void OnHide<T>(T param)
    {
        canvas.gameObject.SetActive(false);
    }
    public override void OnFinal<T>(T param)
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new GameOverParam());
    }
}
