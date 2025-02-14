using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

//生成とリロードtargetが消えたときのショウとハイドがいる　ほとんど継承
//デリゲートでヴェクター3で返すメソッドつくってtargetのポジションを返す
public class TargetUIView : ViewBase
{
    [SerializeField]
    private Image targetUIImage;
    [Header("回転設定＿ループ")]
    [SerializeField] private Vector3 _eulerStartPos;      //リットモーション初期状態
    [SerializeField] private Vector3 _eulerEndPos;        //リットモーション
    [SerializeField] private float _eulerMoveUITimer;     //動かしたい時間
    [SerializeField] private int _eulerLoopCount;         //何回ループするか
    [SerializeField] private LoopType _eulerLoopType;     //どんなループをすのか
    [SerializeField] private Ease _eulerEaseType;         //どんな動きか
    [Header("カラー設定＿ループ")]
    [SerializeField] private Color _colorStart;      //リットモーション初期状態
    [SerializeField] private Color _colorEnd;        //リットモーション
    [SerializeField] private float _colorMoveUITimer;     //動かしたい時間
    [SerializeField] private int _colorLoopCount;         //何回ループするか
    [SerializeField] private LoopType _colorLoopType;     //どんなループをすのか
    [SerializeField] private Ease _colorEaseType;         //どんな動きか
    [Header("拡大縮小設定＿ループ")]
    [SerializeField] private Vector3 _ScaleStart;      //リットモーション初期状態
    [SerializeField] private Vector3 _scaleEnd;        //リットモーション
    [SerializeField] private float _scaleMoveUITimer;     //動かしたい時間
    [SerializeField] private int _scaleLoopCount;         //何回ループするか
    [SerializeField] private LoopType _scaleLoopType;     //どんなループをすのか
    [SerializeField] private Ease _scaleEaseType;         //どんな動きか

    IhasTargetPos targetPos;
    protected override ParamBase GetUseParamBase() => new TargetUIParam();


    public override void OnInit<T>(T param)
    {
        base.OnInit<T>(param);
        TargetUIParam targetUIParam = param as TargetUIParam;
        targetPos = targetUIParam.targetPos;
    }
    public override void OnHide<T>(T param)
    {
        base.OnHide(param);
        //gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
    public override void OnShow<T>(T param)
    {
        base.OnShow(param);
        gameObject.SetActive(true);
    }
    public override void OnFinal<T>(T param)
    {
        base.OnFinal(param);
        Destroy(gameObject);
    }
    //public override void OnFinal<T>(T param)
    //{
    //    //Viewの削除指令を受けたら自身を削除する
    //    Destroy(gameObject);
    //}
    public override void OnAnimation<T>(T param)
    {
        Tweens.ImageLoopRotateTween( targetUIImage.rectTransform,_eulerStartPos, _eulerEndPos, _eulerMoveUITimer, _eulerLoopCount, _eulerLoopType, _eulerEaseType,gameObject);
        Tweens.ImageLoopColorTween(targetUIImage, _colorStart, _colorEnd, _colorMoveUITimer, _colorLoopCount, _colorLoopType, _colorEaseType,gameObject);
        Tweens.ImageLoopScaleTween(targetUIImage.rectTransform, _ScaleStart, _scaleEnd, _scaleMoveUITimer, _scaleLoopCount, _scaleLoopType, _scaleEaseType, gameObject);


        //base.OnAnimation<T>(param);
        //LMotion.Create(new Vector3(0, 0, 0), new Vector3(0, 0, 360), 3f)
        //     .WithLoops(-1, LoopType.Restart)
        //     .WithEase(Ease.Linear)
        //     .BindToEulerAngles(transform);

        //LMotion.Create(new Color(0, 255, 0, 0.3f), new Color(0, 255, 0, 0.9f), 1f)
        //    .WithLoops(-1, LoopType.Yoyo)
        //    .WithEase(Ease.Linear)
        //    .BindToColor(targetUIImage.GetComponent<Image>());

        //LMotion.Create(new Vector3(1.5f, 1.5f, 1.5f), new Vector3(0.5f, 0.5f, 0.5f), 1f) // 1秒かけてスケールを変化
        //    .WithLoops(-1, LoopType.Yoyo)
        //    .WithEase(Ease.Linear)
        //    .BindToLocalScale(transform);// transform.localScaleに紐づけ

    }
    void Update()
    {
        //ここに持ってきたVectorを使ってtargetの場所にUIを置く

        targetUIImage.transform.position = Camera.main.WorldToScreenPoint(targetPos.GetPos);


    }
}
