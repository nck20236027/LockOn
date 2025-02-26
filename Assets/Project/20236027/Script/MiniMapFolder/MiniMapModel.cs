using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//カメラの回転角度を渡す　クォータニオンを使う？　floatだけ？
public class MiniMapModel : MonoBehaviour 
{
    //ロケットの座標
    [Header("ロケットの座標")]
    [SerializeField] private Transform _rocketTransformPosition;
    [Header("ロケットのカメラの回転ｙ軸")]
    [SerializeField] private Transform _cameraTransformRotation;

    //[SerializeField] Transform transformRotation;

    //[SerializeField] Quaternion miniMapRotation;
    Vector3 pos;
    //public IMiniMap miniMapPos => miniMapPos;

    //public Quaternion rotation => miniMapPos.rotation;

    //Transform IMiniMap.Position => miniMapPos.Position;

    private MiniMapParam miniMapParam = new MiniMapParam();
    // Start is called before the first frame update
    void Start()
    {
        //param.cameraTransformPos = this;
        UIMediator.Instance.Init(miniMapParam);
    }

    // Update is called once per frame
    void Update()
    {
        miniMapParam.rocketTransformX = _rocketTransformPosition.transform.position.x;
        miniMapParam.rocketTransformZ = _rocketTransformPosition.transform.position.z;

        miniMapParam.cameraRotationY = _cameraTransformRotation.transform.eulerAngles.y;
        UIMediator.Instance.Reload(miniMapParam);


       // pos = miniMapPos.transform.position;    //

        //pos.x = pos.z;
        //miniMapPos.transform.position = pos;

        //param.Q = Pos.transform.position.y;  //Y座標の受け渡し　float型
        //param.transformRotationHandOver = transformRotation.transform.rotation.y;     //Y回転軸受け渡し

       //UIMediator.Instance.Reload(param);
    }

}
//本来は二つのトランスフォームを渡すだけでいいがバカすぎてできなかった

//カメラはロケットのトランスフォームを取得してそのｙ軸以外同じ動きをする
//カメラの回転はロケットのカメラの回転（Ｙ軸）を取得して　回転させる
//modelはロケットの座標を渡す＆カメラのＹ軸を渡す