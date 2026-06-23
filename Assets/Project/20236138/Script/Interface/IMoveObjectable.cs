using UnityEngine;

/// <summary>
/// 移動可能オブジェクトが実装すべきインターフェイス。
/// - 位置（Transform）と Rigidbody を提供し、ターゲット方向を取得できる。
/// - RocketMove 等の移動ユーティリティから参照される。
/// </summary>
public interface IMoveObjectable
{
    public Transform GetTransform { get; }

    public Rigidbody GetRigidbody { get; }
    // ターゲット位置（ワールド座標）またはターゲット方向ベクトルを返す
    public Vector3 GetTargetVector { get; }
}
