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
    private List<float> GaussLegendreWeights = new List<float>() {                0.3478548f,
                0.6521452f,
                0.6521452f,
                0.3478548f,
    };
    
    //public Vector3 GetOrbit()
    //{

    //}

                private float GetBezierLength(float3 start, float3 startTangent, float3 end, float3 endTangent)
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

    private float3 GetBezier(float3 start, float3 startTangent, float3 end, float3 endTangent, float t)
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
