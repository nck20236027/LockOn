using UnityEngine;

/// <summary>
/// ロックオン対象となるトークン（目標ポイント）を表すコンポーネント。
/// TargetManager に登録され、レンダラーの可視性やワールド位置を提供する。
/// テスト用
/// </summary>
public class TargetToken : MonoBehaviour,ILockTargetable
{
    // レンダラー参照（可視判定に使用）
    private Renderer _renderer;

    public RocketEnergyEffectType EnergyEffectType => RocketEnergyEffectType.Recover;

    /// <summary>
    /// オブジェクトがカメラ視野内にあるかを返すプロパティ
    /// </summary>
    public bool GetIsView { get { return _renderer.isVisible; } }

    /// <summary>
    /// ILockTargetable の Transform 取得実装
    /// </summary>
    public Transform GetTransform => transform;

    // Start でレンダラーをキャッシュして TargetManager に登録する
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        TargetManager.Instance.AddLockTarget(this);
    }

    // Destroy 時に TargetManager から登録解除
    private void OnDestroy()
    {
        TargetManager.Instance.RemoveLockTarget(this);
    }
}
