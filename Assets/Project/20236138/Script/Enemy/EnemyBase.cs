using UnityEngine;

/// <summary>
/// 敵の基底クラス。
/// - 共通のインターフェイス（IDamageable, ILockTargetable）を実装し、派生クラスは具体的な挙動を実装する。
/// - Start/OnDestroy でターゲット管理システムに登録／解除する処理を提供する。
/// </summary>
public abstract class EnemyBase : MonoBehaviour,IDamageable,ILockTargetable
{
    [SerializeField ,Header("最大体力")]
    protected int _enemyMaxHP = 1;
    protected int _enemyNowHP = 1;

    public Transform GetTransform => transform;

    public RocketEnergyEffectType EnergyEffectType => RocketEnergyEffectType.Consume;

    public abstract bool GetIsView { get; }

    protected virtual void Awake()
    {
        _enemyNowHP = _enemyMaxHP;
    }

    protected virtual void Start()
    {
        TargetManager.Instance.AddLockTarget(this);
    }
    protected virtual void OnDestroy()
    {
        TargetManager.Instance?.RemoveLockTarget(this);
    }
    public abstract void Damage(int damage);
}
