using UnityEngine;

/// <summary>
/// 敵弾の速度・威力・寿命等のパラメータをまとめた構造体。
/// </summary>
[System.Serializable]
public struct EnemyBulletStatus
{
    [Header("移動速度")]
    public float moveSpeed ;
    
    [Header("弾の威力")]
    public int bulletPower ;
    
    [Header("寿命（秒）")]
    public float destroyTime ;
}
