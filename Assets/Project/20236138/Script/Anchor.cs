using UnityEngine;

/// <summary>
/// 固定回復ポイントを表すコンポーネント（ロックオン対象）。
/// - ILockTargetable を実装し、ターゲット選択や消費回復値を提供する。
/// - シーン開始時に TargetManager に自身を登録する。
/// </summary>
public class Anchor : MonoBehaviour, ILockTargetable
{
    [SerializeField]
    private float _recoveryValue = 10;

    private Renderer _renderer;

    public Transform GetTransform => transform;

    public bool GetIsView => _renderer.isVisible;

    public RocketEnergyEffectType EnergyEffectType => RocketEnergyEffectType.Recover;

    public float ChangeConsumption => _recoveryValue;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    
    void Start()
    {
        TargetManager.Instance.AddLockTarget(this);
    }

    private void OnDestroy()
    {
        if (TargetManager.Instance != null)
        {
            TargetManager.Instance.RemoveLockTarget(this);
        } 
    }
}
