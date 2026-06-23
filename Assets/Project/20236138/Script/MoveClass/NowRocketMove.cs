using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// ロケット（ロケット風）移動のための補助クラス。
/// ベジェ曲線を使った軌道計算や、現在位置から目標方向へ滑らかに向くための補間を行う。
/// 主に MoveData を受け取り、指定距離移動した先の座標を返す GetOrbit を提供する。
/// </summary>
public class NowRocketMove
{
    // サンプリング粒度（大きいほど正確だが重い）
    private const int BEZIER_SAMPLES = 1000;

    // 4点ガウス・ルジャンドル（n=4）の点・重み（定数）
    private static readonly float[] GaussLegendrePoints = new float[] { -0.8611363f, -0.3399810f, 0.3399810f, 0.8611363f };
    private static readonly float[] GaussLegendreWeights = new float[] { 0.3478548f, 0.6521452f, 0.6521452f, 0.3478548f };

    /// <summary>
    /// ベジェ曲線に沿って移動する軌道計算を行い、指定の移動距離に対応する位置を返す。
    /// - moveData: 始点/終了点と制御点の情報を持つ MoveData
    /// - statu: 移動に関するパラメータ（速度/回転速度など）
    /// - moveDistance: このフレームで移動したい距離（ワールド単位）
    /// 戻り値: 算出された移動先ワールド座標
    /// 注意: この関数は moveData._MovePoint を更新します。
    /// </summary>
    public static Vector3 GetOrbit(MoveData moveData, MoveStatus statu, float moveDistance)
    {
        // 制御点の決定（自然な軌道にするために p1 をスタートのタンジェント、p2 を終点側のタンジェントにする）
        Vector3 p0 = moveData.StartPos.position;
        Vector3 p1 = p0 + moveData.MovePower; // スタート側の制御点
        Vector3 p3 = moveData.EndPos;
        // 終点側の制御点は EndPos に向かうベクトルを逆向きに少し残す（ここでは start 側の MovePower を反転して使う）
        Vector3 p2 = p3 - moveData.MovePower;

        // 現在の t（0..1）を開始点として使う。外部から保持される _MovePoint を尊重する。
        float t = Mathf.Clamp01(moveData.MovePoint);

        // 現在位置をベジェから取得
        Vector3 previousPosition = GetBezier(p0, p1, p2, p3, t);

        // 移動したい総距離（引数で指定された値）
        float remainingToMove = Mathf.Max(0f, moveDistance);

        // 安全上の上限ステップ
        int safety = 0;
        float dt = 1f / BEZIER_SAMPLES;
        Vector3 moveLocation = previousPosition;
        float traveled = 0f;

        // t を少しずつ増やしてセグメント距離を積算、指定距離に達したらセグメント内で補間して止める
        while (traveled < remainingToMove && t < 1f)
        {
            float nextT = Mathf.Min(1f, t + dt);
            Vector3 nextPos = GetBezier(p0, p1, p2, p3, nextT);
            float segDist = Vector3.Distance(previousPosition, nextPos);

            if (segDist <= Mathf.Epsilon)
            {
                // ほとんど動かない場合は t を進めて次へ
                t = nextT;
                previousPosition = nextPos;
                moveLocation = nextPos;
            }
            else if (traveled + segDist >= remainingToMove)
            {
                // セグメント内で止めるため補間
                float need = remainingToMove - traveled;
                float ratio = Mathf.Clamp01(need / segDist);
                float interpT = Mathf.Lerp(t, nextT, ratio);
                moveLocation = GetBezier(p0, p1, p2, p3, interpT);
                t = interpT;
                traveled = remainingToMove;
                previousPosition = moveLocation;
                break;
            }
            else
            {
                traveled += segDist;
                t = nextT;
                previousPosition = nextPos;
                moveLocation = nextPos;
            }

            safety++;
            if (safety > BEZIER_SAMPLES + 5)
            {
                // 何かおかしければループを抜ける
                break;
            }
        }

        // t を保存（次フレームの開始 t）
        moveData.MovePoint = t;

        // 回転処理: 目標方向に応じて直接向けるか回転しつつ前進する
        Vector3 moveTargetDirection = (moveLocation - moveData.StartPos.position);
        bool hasTargetDir = moveTargetDirection.sqrMagnitude > 1e-6f;
        moveTargetDirection = hasTargetDir ? moveTargetDirection.normalized : moveData.StartPos.forward;

        Quaternion lookRotation = Quaternion.LookRotation(moveTargetDirection, Vector3.up);

        float angle = Quaternion.Angle(moveData.StartPos.rotation, lookRotation);

        Vector3 resultPos;

        // 回転閾値として statu.Damping を角度(deg)として使う（小さいほど素早く向く）
        if (angle <= statu.Damping)
        {
            // ほぼ向いている -> 直接位置をベジェ上の位置に置く
            moveData.StartPos.rotation = lookRotation;
            resultPos = moveLocation;
        }
        else
        {
            // 回転しながら前進（前方に向いたままの前進量は remainingToMove）
            // 回転速度は statu.Damping（deg/sec）と仮定
            moveData.StartPos.rotation = Quaternion.RotateTowards(moveData.StartPos.rotation, lookRotation, statu.Damping * Time.deltaTime);

            // 前進方向は常に StartPos.forward（回転途中の向き）
            resultPos = moveData.StartPos.position + moveData.StartPos.forward * remainingToMove;
        }

        return resultPos;
    }

    /// <summary>
    /// ベジェ曲線の弧長を数値積分で求める（区間 [0,1]）。導関数の大きさをガウス・ルジャンドル積分で評価する。
    /// </summary>
    private static float GetBezierLength(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // 4点ガウス・ルジャンドルを用いる
        float a = 0f, b = 1f;
        float half = 0.5f * (b - a);
        float center = 0.5f * (b + a);
        float sum = 0f;

        for (int i = 0; i < GaussLegendrePoints.Length; i++)
        {
            float xi = GaussLegendrePoints[i];
            float wi = GaussLegendreWeights[i];
            float t = center + half * xi; // 変換
            Vector3 deriv = GetBezierDerivative(p0, p1, p2, p3, t);
            sum += wi * deriv.magnitude;
        }

        return sum * half;
    }

    /// <summary>
    /// 3次ベジェ曲線の点を返す
    /// </summary>
    private static Vector3 GetBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        return u * u * u * p0 + 3f * u * u * t * p1 + 3f * u * t * t * p2 + t * t * t * p3;
    }

    /// <summary>
    /// ベジェ曲線の導関数 B'(t)
    /// </summary>
    private static Vector3 GetBezierDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        // 3(1-t)^2 (p1-p0) + 6(1-t)t (p2-p1) + 3 t^2 (p3-p2)
        return 3f * u * u * (p1 - p0) + 6f * u * t * (p2 - p1) + 3f * t * t * (p3 - p2);
    }

    // 既存の補助回転関数は残す（必要なら外部で使える）
    private static Quaternion RotateTowards(Quaternion current, Quaternion target, float deltaAngle)
    {
        var dot = Mathf.Abs(Quaternion.Dot(current, target));
        var radian = Mathf.Acos(Mathf.Min(dot, 1f));
        var angle = math.degrees(radian);

        if (angle <= Mathf.Epsilon) return target;
        var t = Mathf.Min(1f, deltaAngle / angle);
        return Quaternion.Slerp(current, target, t);
    }
}
