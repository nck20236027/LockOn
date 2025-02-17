using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeCoordinator : IVolumeCoordinater
{
    private float hearingDistance;
    private float panScale;

    public VolumeCoordinator(float hearingDistance, float panScale)
    {
        this.hearingDistance = hearingDistance;
        this.panScale = panScale;
    }

    /// <summary>
    /// 距離で調整した音量
    /// </summary>
    /// <param name="speaker"></param>
    /// <returns></returns>
    public float GetAdjustVolume(IListener listener,ISpeaker speaker)
    {
        // プレイヤーとの距離を計算
        float distance = Vector3.Distance(speaker.SpeakerPos, listener.ListenerPos);
        float volume = Mathf.Clamp01(1 - (distance / hearingDistance)); // 距離に応じた音量
        return volume;
    }

    /// <summary>
    /// 位置関係で調整したPanの値を返す
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="speaker"></param>
    /// <returns></returns>
    public float GetAdjustPan(IListener listener, ISpeaker speaker)
    {
        // プレイヤーの位置との角度計算
        Vector3 toListener = speaker.SpeakerPos - listener.ListenerPos;
        toListener.y = 0;

        // 正規化されたパン値を計算（-1.0 ? 1.0 の範囲に収める）
        float pan = Mathf.Clamp(toListener.x / panScale, -1f, 1f); // 5f は適当なスケール値（調整可能）

        return pan;
    }
}
