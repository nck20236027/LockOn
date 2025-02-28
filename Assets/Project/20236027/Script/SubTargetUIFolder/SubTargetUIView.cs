using LitMotion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//オブジェクトプールを使ってUIを使いまわす
//
public class SubTargetUIView : ViewBase
{
    public Image targetUIImage ;
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
    [Header("オブジェクトプール")]
    [SerializeField] TestObjectPool _testObjectPool;
    ISubTargetUI subTargetUI;
    protected override ParamBase GetUseParamBase() => new SubTargetUIParam();
    List<Image> subTargetUIImage = new();

    //a uiいれた

    public override void OnInit<T>(T param)
    {
        base.OnInit(param);
        SubTargetUIParam subTargetUIParam = param as SubTargetUIParam;
        subTargetUI = subTargetUIParam.subTargetUI;
        Image ImageUI = _testObjectPool.GetPool();
        subTargetUIImage.Add(ImageUI);
        Tweens.ImageLoopRotateTween(ImageUI.rectTransform, _eulerStartPos, _eulerEndPos, _eulerMoveUITimer, _eulerLoopCount, _eulerLoopType, _eulerEaseType, ImageUI.gameObject);
        Tweens.ImageLoopColorTween(ImageUI, _colorStart, _colorEnd, _colorMoveUITimer, _colorLoopCount, _colorLoopType, _colorEaseType, ImageUI.gameObject);
        Tweens.ImageLoopScaleTween(ImageUI.rectTransform, _ScaleStart, _scaleEnd, _scaleMoveUITimer, _scaleLoopCount, _scaleLoopType, _scaleEaseType, ImageUI.gameObject);
    }
    public override void OnReload<T>(T param)
    {
        base.OnReload(param);
        for (int i = 0; i < subTargetUI.ISubTargetUIList.Count; ++i)
        {
            //aとsubTargetUIの大きさを比べて 足りない分のオブジェクトを追加　GetPool()
            if (subTargetUIImage.Count < subTargetUI.ISubTargetUIList.Count)
            {
                Image ImageUI = _testObjectPool.GetPool();
                subTargetUIImage.Add(ImageUI);

                Tweens.ImageLoopRotateTween(ImageUI.rectTransform, _eulerStartPos, _eulerEndPos, _eulerMoveUITimer, _eulerLoopCount, _eulerLoopType, _eulerEaseType, ImageUI.gameObject);
                Tweens.ImageLoopColorTween(ImageUI, _colorStart, _colorEnd, _colorMoveUITimer, _colorLoopCount, _colorLoopType, _colorEaseType, ImageUI.gameObject);
                Tweens.ImageLoopScaleTween(ImageUI.rectTransform, _ScaleStart, _scaleEnd, _scaleMoveUITimer, _scaleLoopCount, _scaleLoopType, _scaleEaseType, ImageUI.gameObject);


            }
            subTargetUIImage[i].transform.position = Camera.main.WorldToScreenPoint(subTargetUI.ISubTargetUIList[i]);

        }
        for( int i = subTargetUI.ISubTargetUIList.Count; i < subTargetUIImage.Count; ++i)
        {
            _testObjectPool.SetupPool(subTargetUIImage[i]);
            subTargetUIImage.Remove(subTargetUIImage[i]);


        }
    }
    public override void OnFinal<T>(T param)
    {
        base.OnFinal(param);

    }


    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new SubTargetUIParam()); //すべてのviewにこれを書かないとバグる
    }
}
