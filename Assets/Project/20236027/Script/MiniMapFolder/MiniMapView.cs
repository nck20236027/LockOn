using UnityEngine;
//プレイヤーのやつはあとでマップの
//カメラはロケットのトランスフォームを取得してそのｙ軸以外同じ動きをする
//カメラの回転はロケットのカメラの回転（Ｙ軸）を取得して　回転させる
//modelはロケットの座標を渡す＆カメラのＹ軸を渡す
public class MiniMapView : ViewBase
{
    [Header("ミニマップのゲームオブジェクト")]
    [SerializeField] private Transform miniMapTransform;
    [SerializeField] private float miniMapScale;
    //[Header("詳細")]
    //[SerializeField] private float _miniMapScale;
    //private float _cameraRotation;
    Vector3 pos;
    Vector3 cameraRotation;


    protected override ParamBase GetUseParamBase() => new MiniMapParam();
    void Start()
    {
        //miniMapRotation = 
        //miniMapRotation = new Quaternion();
    }
    public override void OnInit<T>(T param)
    {
        base.OnInit(param);
        MiniMapParam miniMapParam = param as MiniMapParam;
    }
    public override void OnReload<T>(T param)
    {
        base.OnReload(param);
        MiniMapParam miniMapParam = param as MiniMapParam;
        pos = miniMapTransform.transform.position;//ここは座標指定
        pos.x = miniMapParam.rocketTransformX;
        pos.y = miniMapScale;//適当
        pos.z = miniMapParam.rocketTransformZ;
        miniMapTransform.transform.position = pos;

        cameraRotation = miniMapTransform.transform.eulerAngles;//ここは回転指定
        cameraRotation.y = miniMapParam.cameraRotationY;
        miniMapTransform.transform.rotation = Quaternion.Euler(cameraRotation);



        //_cameraRotation = miniMapParam.transformRotationHandOver;
        //miniMapObj.transform.localEulerAngles.y = _cameraRotation;
        //miniMapTransform.transform.rotation = miniMapParam.cameraTransformPos.rotation;//これはｙ軸だけに遅れていない

    }
    public override void OnHide<T>(T param)
    {
        base.OnHide(param);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
