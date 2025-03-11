using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NowRocketMove 
{
    private const int BEZIER_SAMPLES = 10000;
    private  static List<float> GaussLegendrePoints = new List<float>() {  
        -0.8611363f,
                -0.3399810f,
                0.3399810f,
                0.8611363f,};
    private static List<float> GaussLegendreWeights = new List<float>() {                
        0.3478548f,
                0.6521452f,
                0.6521452f,
                0.3478548f,
    };

    public static Vector3 GetOrbit(MoveData _moveData,MoveStatus _statu,float _moveDistance)
    {
        var length = GetBezierLength(
    _moveData.StartPos.position,
    _moveData.StartPos.position + _moveData.MovePowor,
    _moveData.EndPos,
    _moveData.EndPos -(_moveData.EndPos - _moveData.StartPos.position)
);

        // 0を起点に前回の位置を線分だった場合に置き換えて取得する
        var previousLength = Vector3.Lerp(Vector3.zero, _moveData.StartPos.position,_moveData._MovePoint).magnitude;
        // このフレームで変化したベジェ曲線の長さに合わせた移動量を求める
        float latestT = math.unlerp(0, length, previousLength);
        latestT = Mathf.Clamp(latestT,0,1);   // 0~1に丸める

        // 現在の速度で1フレームに進む量
        var moveAmount = _statu.MaxSpeed * Time.deltaTime;

        // whileで使う変数
        // サンプルの数分移動した距離
        var traveledDistance = 0f;
        // 移動場所の結果(リザルト)
        var moveLocation = Vector3.zero;
        // 一つ前のwhileループで取得した座標(現在の遷移割合で初期化)
        var previousPosition = GetBezier(
    _moveData.StartPos.position,
    _moveData.StartPos.position + _moveData.MovePowor,
    _moveData.EndPos,
    _moveData.EndPos - _moveData.StartPos.position,
                latestT
            );
        var safety = 0;
        while (traveledDistance < moveAmount)
        {
            // サンプル数分徐々に増やしていく
            latestT += 1f / BEZIER_SAMPLES;
            latestT = math.saturate(latestT);

            // ベジェ曲線の現在座標を求める
            moveLocation = GetBezier(
    _moveData.StartPos.position,
    _moveData.StartPos.position + _moveData.MovePowor,
    _moveData.EndPos,
    _moveData.EndPos - _moveData.StartPos.position,
                latestT
            );

            // 現在位置からどれだけ移動できたか求める
            traveledDistance += Mathf.Abs((moveLocation - previousPosition).magnitude);
            // 今回の位置を保存
            previousPosition = moveLocation;

            // 無限ループ安全処理
            safety++;
            if (BEZIER_SAMPLES <= safety)
            {
                // サンプル数を超えたループ回数に達したらここで終了する
                break;
            }
        }

        // 現在の進行方向を取得
        var moveForward = _moveData.StartPos.forward;
        // 現在の位置から移動先までの方向を求める
        Vector3 moveTargetDirection = (moveLocation - _moveData.StartPos.position).normalized;
        var cosHalf = math.cos(_statu.Damping / 2 * Mathf.Rad2Deg);
        var innerProduct = Vector3.Dot(moveForward, moveTargetDirection);

        var lookRotation = Quaternion.LookRotation(moveTargetDirection, Vector3.up);
        _moveData._MovePoint = latestT;
        Vector3 _movePos = Vector3.zero;
        if (cosHalf < innerProduct)
        {
            // 次の地点が旋回可能な範囲内であれば次の地点の方向へ機体を傾ける
            _moveData.StartPos.rotation = lookRotation;
            // 移動地点を更新する
            _movePos = moveLocation;
        }
        else
        {
            // 限界まで回転させる
            _moveData.StartPos.rotation = RotateTowards(
                _moveData.StartPos.rotation,
                lookRotation,
                _statu.Damping * Time.deltaTime);


            // 現在の向いている方向へ進む
            _movePos = _moveData.StartPos.position + _moveData.StartPos.transform.forward * _statu.MaxSpeed * Time.deltaTime;
        }

        // 処理終了時の遷移状態を保存


        return _movePos;
    }

    private static float GetBezierLength(Vector3 start, Vector3 startTangent, Vector3 end,Vector3 endTangent)
    {
        // 結果となる長さの初期値
        var length = 0f;

        for (int i = 0; i < GaussLegendrePoints.Count; i++)
        {
            // 各点のベジェ座標から長さを求める
            var t = 0.5f * (GaussLegendrePoints[i] + 1f);
            var bezierCurve = GetBezier(start, startTangent, end, endTangent, t);
            length += GaussLegendreWeights[i] * math.length(bezierCurve);
        }

        // 計算の半分を結果として返す
        return length * 0.5f;
    }

    private static Vector3 GetBezier(Vector3 start, Vector3 startTangent, Vector3 end, Vector3 endTangent, float t)
    {
        // 遷移を反転
        var oneMinus = 1f - t;
        // ベジェ曲線を計算した結果を返す
        return
              Mathf.Pow(oneMinus, 3) * start
            + 3f * Mathf.Pow(oneMinus, 2) * t * startTangent
            + 3f * oneMinus * Mathf.Pow(t, 2) * endTangent
            + Mathf.Pow(t, 3) * end;
    }

    private static Quaternion RotateTowards(Quaternion current, Quaternion target, float deltaAngle)
    {
        // 目標までの回転角度を計算
        var dot = Mathf.Abs(Quaternion.Dot(current, target));
        var radian = Mathf.Acos(Mathf.Min(dot, 1f));
        var angle = math.degrees(radian);

        var t = Mathf.Min(1f, deltaAngle / angle);
        return Quaternion.Slerp(current, target, t);
    }
}

public class MoveData
{
    public Transform StartPos;
    public Vector3 MovePowor;

    public Vector3 EndPos;
    public float _MovePoint = 0;
    public MoveData(Transform StartPos,Vector3 EndPos,Vector3 movePowor)
    {
        this.StartPos = StartPos;
        this.MovePowor = movePowor;
        this.EndPos = EndPos;
    }
    public MoveData(MoveData data)
    {
        _MovePoint = data._MovePoint;
        StartPos = data.StartPos;
        MovePowor = data.MovePowor; 
        EndPos = data.EndPos;

    }
}