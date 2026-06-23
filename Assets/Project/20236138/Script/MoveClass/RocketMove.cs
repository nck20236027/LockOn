using UnityEngine;

/// <summary>
/// ロケット系の移動計算を行うユーティリティクラス（RocketMove）。
/// 目標方向への遠心/向心力的な力を計算して速度に加算し、回転補間を行う。
/// IMoveObjectable と MoveStatus を受け取り、FixedUpdate ごとに呼び出して物理挙動を更新する。
/// </summary>
public class RocketMove
{
    /// <summary>
    /// 指定オブジェクトを MoveStatus のパラメータに従って移動させる。
    /// - moveObject: IMoveObjectable（位置・剛体・ターゲット情報を持つオブジェクト）
    /// - status: 移動に関する各種パラメータ（MaxSpeed, Bendability, Propulsion, Damping, Curve など）
    /// - time: ステート経過時間（Curve の評価に使用）
    /// </summary>
    public static void MoveTarget(IMoveObjectable moveObject,MoveStatus status,float time)
    {
        Vector3 toTarget = moveObject.GetTargetVector != Vector3.zero ?
            moveObject.GetTargetVector - moveObject.GetTransform.position : moveObject.GetTransform.up;
        Vector3 vn = moveObject.GetRigidbody.velocity.normalized;
        float dot = Vector3.Dot(toTarget, vn);
        Vector3 centripetalAccel = toTarget - (vn * dot);
        float centripetalAccelMagnitude = centripetalAccel.magnitude;
        
        if (centripetalAccelMagnitude > 1f)
        {
            centripetalAccel /= centripetalAccelMagnitude;
        }

        // Bendability が 0 の場合でも物理値が NaN/Infinity にならないように下限を設ける。
        float bendability = Mathf.Max(0.001f, status.Bendability);
        // 中心向きの加速度成分（制御）
        Vector3 force = centripetalAccel * Mathf.Pow(status.MaxSpeed, 2) / bendability;
        // 前方への推進力を加える
        force += vn * status.Propulsion;
        // 減衰（速度に比例）を引く
        force -= moveObject.GetRigidbody.velocity * status.Damping;
        // カーブ評価を先に取得してから乗算する（効率化）
        float curveValue = status.Curve.Evaluate(time);
        moveObject.GetRigidbody.velocity += force * curveValue * Time.fixedDeltaTime;
        // 回転補間：現在の回転から速度方向へ滑らかに向ける
        moveObject.GetTransform.rotation =
        Quaternion.Lerp(moveObject.GetTransform.rotation,
            Quaternion.FromToRotation(Vector3.up,
            curveValue == 0f ? toTarget : moveObject.GetRigidbody.velocity), bendability);
    }
}
