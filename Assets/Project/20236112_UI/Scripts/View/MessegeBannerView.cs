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

    [SerializeField, Header("フェードインカラー")]
    private Color _fadeInColor;
    [SerializeField, Header("フェードアウトアウトカラー")]
    private Color _fadeOutColor;

    [SerializeField, Header("フェードインに掛かる時間")]
    private float _fadeInTime = 0.1f;
    [SerializeField, Header("フェードアウトに掛かる時間")]
    private float _fadeOutTime = 0.1f;
    [SerializeField, Header("テキストの遷移時間")]
    private float _transitionTime = 1f;
    [SerializeField, Header("待機時間")]
    private float _waitTime;

    [SerializeField]
    private LitMotion.Ease _ease;

    protected override ParamBase GetUseParamBase() => new BannerParam();

    public async override void OnShow<T>(T param)
    {
        Time.timeScale = 0f;
        canvas.gameObject.SetActive(true);
        var bannerParam = param as BannerParam;

        _titleText.text = bannerParam.titleText;
        _mainText.text = bannerParam.mainText;

        await UniTask.WaitForSeconds(_waitTime, ignoreTimeScale: true);

        //フェードイン
        Tweens.ImageColorTween(_bannerImage, _fadeInColor, _fadeOutColor, _fadeInTime, _ease, gameObject);
        Tweens.TextColorTween(_titleText, _fadeInColor, _fadeOutColor, _fadeInTime, _ease, gameObject);
        Tweens.TextColorTween(_mainText, _fadeInColor, _fadeOutColor, _fadeInTime, _ease, gameObject);
        await UniTask.WaitForSeconds(_waitTime, ignoreTimeScale:true);

        //テキスト遷移
        Tweens.TextTransformTween(_bannerRectTransform, _startRectTransform, _middleRectTransform, _transitionTime, _ease, gameObject);
        await UniTask.WaitForSeconds(_waitTime, ignoreTimeScale: true);

        Tweens.TextTransformTween(_bannerRectTransform, _middleRectTransform, _endRectTransform, _transitionTime, _ease, gameObject);
        await UniTask.WaitForSeconds(_waitTime, ignoreTimeScale:true);


        //フェードアウト
        Tweens.ImageColorTween(_bannerImage, _fadeOutColor, _fadeInColor, _fadeOutTime, _ease, gameObject);
        Tweens.TextColorTween(_titleText, _fadeOutColor, _fadeInColor, _fadeOutTime, _ease, gameObject);
        Tweens.TextColorTween(_mainText, _fadeOutColor, _fadeInColor, _fadeOutTime, _ease, gameObject);
        Time.timeScale = 1f;
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
        ServiceLocator<UIMediator>.GetInstance().Final(new BannerParam());
        Debug.Log("MessegeBannerViewFinal");

    }
}