using LitMotion;
using UnityEngine;
using UnityEngine.UI;

public class GameEndView : ViewBase
{
    [Header("対象のテキストを入れる")]
    [SerializeField]
    private Text[] _texts;
    [SerializeField]
    private RectTransform[] _textRectTransforms;

    [SerializeField]
    private Color _tergetColor;

    [Header("選択時の色")]
    [SerializeField]
    private Color _unselectColor = Color.black;
    [SerializeField]
    private Color _selectColor = Color.white;

    [Header("遷移時間")]
    [SerializeField]
    private float expandTime = 0.1f;
    [SerializeField]
    private float reduceTime = 0.1f;

    [Header("遷移方法")]
    [SerializeField]
    private Ease changeEase = Ease.Linear;

    private int _initFointSize = 60;             //生成時のフォントサイズ
    private int _tergetFontSize = 80;            //変更後のフォントサイズ

    private int _beforeIndex;

    protected override ParamBase GetUseParamBase() => new GameEndParam();

    public override void OnInit<T>(T param)
    {
        canvas.gameObject.SetActive(false);
        GameEndParam gameEndParam = param as GameEndParam;

        Text firstSelectButton = _texts[gameEndParam.currentIndex];

        Tweens.FontSizeTween(firstSelectButton, _initFointSize, _tergetFontSize, expandTime, changeEase, gameObject);
        Tweens.TextColorTween(firstSelectButton, _tergetColor, _tergetColor, expandTime, changeEase, gameObject);

        _beforeIndex = gameEndParam.currentIndex;
    }

    public override void OnReload<T>(T param)
    {
        var gameEndParam = param as GameEndParam;

        Text tweenText = _texts[gameEndParam.currentIndex];
        Text beforeText = _texts[_beforeIndex];

        RectTransform rectTransform = _textRectTransforms[gameEndParam.currentIndex];

        Tweens.TextColorTween(beforeText, _tergetColor, _unselectColor, expandTime, changeEase, gameObject);
        Tweens.FontSizeTween(beforeText, _tergetFontSize, _initFointSize, reduceTime, changeEase, gameObject);

        Tweens.TextColorTween(tweenText, _tergetColor, _selectColor, expandTime, changeEase, gameObject);
        Tweens.FontSizeTween(tweenText, _initFointSize, _tergetFontSize, expandTime, changeEase, gameObject);

        _beforeIndex = gameEndParam.currentIndex;

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
        //Viewの削除指令を受けたら自身を削除する
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new GameEndParam());
        Debug.Log("GameEndViewFinal");

    }
}
