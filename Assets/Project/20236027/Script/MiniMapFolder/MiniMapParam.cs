public class MiniMapParam : ParamBase
{//Vector3で渡すかトランスフォーム

    public float rocketTransformX;  //カメラはロケットのトランスフォームを取得
    public float rocketTransformZ;

    public float cameraRotationY;   //ロケットのカメラの回転（Ｙ軸）を取得

}
//カメラはロケットのトランスフォームを取得してそのｙ軸以外同じ動きをする
//カメラの回転はロケットのカメラの回転（Ｙ軸）を取得して　回転させる
//modelはロケットの座標を渡す＆カメラのＹ軸を渡す
