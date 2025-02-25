using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MessegeBannerView : ViewBase
{
    [SerializeField]
    private Image _bannerImage;
    [SerializeField]
    private Text _titleText;
    [SerializeField]
    private Text _mainText;


    [SerializeField]
    private RectTransform _bannerRectTransform;
    [SerializeField]
    private Vector3 _startRectTransform;
    [SerializeField]
    private Vector3 _middleRectTransform;
    [SerializeField]
    private Vector3 _endRectTransform;

    [SerializeField, Header("フェードイン")]
    private Color _fadeInColor;
    [SerializeField, Header("フェードアウト")]
    private Color _fadeOutColor;

    [SerializeField, Header("フェードに掛かる時間")]
    private float _direction = 0.1f;
    [SerializeField, Header("テキストの遷移時間")]
    private float _transitionTime = 1f;
    [SerializeField, Header("テキストの待機時間")]
    private float _waitTime = 3f;

    [SerializeField]
    private LitMotion.Ease _ease;

    protected override ParamBase GetUseParamBase() => new BannerParam();


    public async override void OnInit<T>(T param)
    {
        canvas.gameObject.SetActive(true);
        var bannerParam = param as BannerParam;

        _titleText.text = bannerParam.titleText;
        _mainText.text = bannerParam.mainText;

        //フェードイン
        Tweens.ImageColorTween(_bannerImage, _fadeInColor, _fadeOutColor, _direction, _ease, gameObject);
        Tweens.TextColorTween(_titleText, _fadeInColor, _fadeOutColor, _direction, _ease, gameObject);
        Tweens.TextColorTween(_mainText, _fadeInColor, _fadeOutColor, _direction, _ease, gameObject);
        await UniTask.WaitForSeconds(2);

        //テキスト遷移
        Tweens.TextTransformTween(_bannerRectTransform, _startRectTransform, _middleRectTransform, _direction, _ease, gameObject);
        await UniTask.WaitForSeconds(2);

        Tweens.TextTransformTween(_bannerRectTransform, _middleRectTransform, _endRectTransform, _direction, _ease, gameObject);
        await UniTask.WaitForSeconds(2);


        //フェードアウト
        Tweens.ImageColorTween(_bannerImage, _fadeOutColor, _fadeInColor, _direction, _ease, gameObject);
        Tweens.TextColorTween(_titleText, _fadeOutColor, _fadeInColor, _direction, _ease, gameObject);
        Tweens.TextColorTween(_mainText, _fadeOutColor, _fadeInColor, _direction, _ease, gameObject);

    }

    public override void OnReload<T>(T param)
    {

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
}
