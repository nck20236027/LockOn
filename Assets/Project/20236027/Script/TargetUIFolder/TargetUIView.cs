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
    }
    public override void OnShow<T>(T param)
    {
        base.OnShow(param); 
    }
    public override void OnFinal<T>(T param)
    {
        base.OnFinal(param);
    }
    //public override void OnFinal<T>(T param)
    //{
    //    //Viewの削除指令を受けたら自身を削除する
    //    Destroy(gameObject);
    //}
    public override void OnAnimation<T>(T param)
    {
        base.OnAnimation<T>(param);
        LMotion.Create(new Vector3(0, 0, 0), new Vector3(0, 0, 360), 3f)
             .WithLoops(-1, LoopType.Restart)
             .WithEase(Ease.Linear)
             .BindToEulerAngles(transform);

        LMotion.Create(new Color(0, 255, 0, 0.3f), new Color(0, 255, 0, 0.9f), 1f)
            .WithLoops(-1, LoopType.Yoyo)
            .WithEase(Ease.Linear)
            .BindToColor(targetUIImage.GetComponent<Image>());

        LMotion.Create(new Vector3(1.5f, 1.5f, 1.5f), new Vector3(0.5f, 0.5f, 0.5f), 1f) // 1秒かけてスケールを変化
            .WithLoops(-1, LoopType.Yoyo)
            .WithEase(Ease.Linear)
            .BindToLocalScale(transform);// transform.localScaleに紐づけ

    }
    void Update()
    {
        //ここに持ってきたVectorを使ってtargetの場所にUIを置く
        
        targetUIImage.transform.position = Camera.main.WorldToScreenPoint(targetPos.GetPos);


    }
}
