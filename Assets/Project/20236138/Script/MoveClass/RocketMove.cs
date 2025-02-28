using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NowRocketMove 
{
    private const int BEZIER_SAMPLES = 10000;
    private List<float> GaussLegendrePoints = new List<float>() {  
        -0.8611363f,
                -0.3399810f,
                0.3399810f,
                0.8611363f,};
    private List<float> GaussLegendreWeights = new List<float>() {                
        0.3478548f,
                0.6521452f,
                0.6521452f,
                0.3478548f,
    };

//    public Vector3 GetOrbit(MoveData _moveData,MoveStatus _statu)
//    {
//        var length = GetBezierLength(
//    _moveData.StartPos.position,
//    _moveData.StartPos.position + _moveData.MovePowor,
//    _moveData.EndPos.position,
//    _moveData.EndPos.position - _moveData.StartPos.position
//);

//        // 0を起点に前回の位置を線分だった場合に置き換えて取得する
//        var previousLength = Mathf.Lerp(0, _moveData.StartPos.position,0);
//        // このフレームで変化したベジェ曲線の長さに合わせた移動量を求める
//        var latestT = math.unlerp(0, length, previousLength);
//        latestT = math.saturate(latestT);   // 0~1に丸める

//        // 現在の速度で1フレームに進む量
//        var moveAmount = _statu.MaxSpeed * Time.deltaTime;

//        // whileで使う変数
//        // サンプルの数分移動した距離
//        var traveledDistance = 0f;
//        // 移動場所の結果(リザルト)
//        var moveLocation = float3.zero;
//        // 一つ前のwhileループで取得した座標(現在の遷移割合で初期化)
//        var previousPosition = GetBezier(
//    _moveData.StartPos.position,
//    _moveData.StartPos.position + _moveData.MovePowor,
//    _moveData.EndPos.position,
//    _moveData.EndPos.position - _moveData.StartPos.position
//                latestT
//            );
//        var safety = 0;
//    }

    private float GetBezierLength(Vector3 start, Vector3 startTangent, Vector3 end,Vector3 endTangent)
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

    private float3 GetBezier(Vector3 start, Vector3 startTangent, Vector3 end, Vector3 endTangent, float t)
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
}

public class MoveData
{
    public Transform StartPos;
    public Vector3 MovePowor;

    public Transform EndPos;
}