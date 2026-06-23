using UnityEngine;

/// <summary>
/// ターゲットロックオン対象
/// </summary>
public interface ILockTargetable
{
    /// <summary>
    /// ロックオン時の燃料の変化タイプ
    /// </summary>
    public RocketEnergyEffectType EnergyEffectType {get;}

    /// <summary>
    /// 座標取得
    /// </summary>
    public Transform GetTransform { get; }
    
    /// <summary>
    /// 画面内に入っているかを取得
    /// </summary>
    public bool GetIsView { get; }
}

