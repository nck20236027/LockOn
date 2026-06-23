using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// 三角形型の敵（TriangleEnemy）。
/// - プレイヤー接近時に一定時間後に自爆するタイプと追跡するタイプがある。
/// - RocketMove を利用して追跡/移動を行い、破壊時に範囲ダメージを与える。
/// </summary>
public class TriangleEnemy :EnemyBase,IMoveObjectable
{
    // Inspector で設定する値
    [SerializeField, Header("探索距離")]
    private float _searchDistance = 10;

    [SerializeField, Header("自爆までの時間")]
    private float _timeDestruction = 4f;

    [SerializeField, Header("追跡速度")]
    private float _chaseSpeed = 10;

    [SerializeField, Header("自爆ダメージ")]
    private int _selfDestructionDamage = 10;

    [SerializeField, Header("自爆範囲スケール")]
    private float _selfDestructionScale = 10;

    [SerializeField, Header("自爆発動までの時間")]
    private float _selfDestructionSpeed = 10;

    [SerializeField]
    private LayerMask _mask;

    [SerializeField]
    private AudioClip _alarmSound;

    [SerializeField]
    private AudioClip _deathSound;

    [SerializeField]
    private MoveStatus _moveStatus;

    [SerializeField]
    private Renderer _renderer;

    // 内部状態
    private float _nowTimeDestruction = 0;
    private CancellationToken _token;
    private EnemyDeadParam _deadParam = new();
    private Rigidbody _rb;
    private bool _isTracking = false;

    // 公開プロパティ
    public override bool GetIsView => _renderer.isVisible;
    public Transform GetTransform => transform;
    public Rigidbody GetRigidbody => _rb;
    public Vector3 GetTargetVector => TargetManager.Instance.GetPlayerPos ;

    public override void Damage(int damage)
    {
        _enemyNowHP -= damage;
        
        if(_enemyNowHP > 0) return;
        
        ServiceLocator<SEManager>.GetInstance().PlaySound(_deathSound, true);
        _deadParam.enemyDeadPosition = transform.position;
        ServiceLocator<UIMediator>.GetInstance().Animation(_deadParam);
        Destroy(gameObject);
    }

    /// <summary>
    /// 自爆する時間や動きの性能などのステータスをセットする関数
    /// </summary>
    /// <param name="selfDestructionDamage"></param>
    /// <param name="timeDestruction"></param>
    /// <param name="chaseSpeed"></param>
    /// <param name="status"></param>
    /// <param name="isTracking"></param>
    public void SetStatus(int selfDestructionDamage , float timeDestruction,float chaseSpeed,MoveStatus status,bool isTracking)
    {
        _selfDestructionDamage = selfDestructionDamage;
        _timeDestruction = timeDestruction;
        _chaseSpeed = chaseSpeed;
        _moveStatus = status;
        _isTracking = isTracking;
    }

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        base.Start();
        _token = this.GetCancellationTokenOnDestroy();
        _deadParam.enemyDeadPosition = transform.position;
        ServiceLocator<UIMediator>.GetInstance().Init(_deadParam);
    }

    private void FixedUpdate()
    {
        RocketMove.MoveTarget(this, _moveStatus, _nowTimeDestruction);
        if ((GetTransform.position - GetTargetVector).sqrMagnitude < Mathf.Pow(_searchDistance, 2))
        {
            if (!_isTracking)
            {
                AttackAsync(_token).Forget();
            }
            _nowTimeDestruction += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// 自爆攻撃の待機、警告音、破壊処理を順に実行する。
    /// </summary>
    private async UniTask AttackAsync(CancellationToken cancellationToken)
    {
        try
        {
            Attack();
            float waitTime = Mathf.Max(0f, _timeDestruction - _selfDestructionSpeed);
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime),cancellationToken:cancellationToken);
            ServiceLocator<SEManager>.GetInstance().PlaySound(_alarmSound, true);
            await UniTask.Delay(TimeSpan.FromSeconds(_selfDestructionSpeed), cancellationToken: cancellationToken);
            _deadParam.enemyDeadPosition = transform.position;
            ServiceLocator<UIMediator>.GetInstance().Animation(_deadParam);
            Destroy(gameObject);
        }
        catch (OperationCanceledException)
        {
            // キャンセルは正常な終了として扱うため、何もしない。
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    /// <summary>
    /// 自爆攻撃の追跡状態へ切り替える。
    /// </summary>
    private void Attack()
    {
        _isTracking = true;
        _moveStatus.MaxSpeed = _chaseSpeed;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _selfDestructionScale, Vector3.forward,0.0001f,_mask);
        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable damage = hits[i].transform.GetComponent<IDamageable>();
            if(damage != null)
            {
                damage.Damage(_selfDestructionDamage);
            }
        }

    }
}
