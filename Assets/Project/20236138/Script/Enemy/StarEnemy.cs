using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// 星型の敵（StarEnemy）。
/// - 一定距離内でプレイヤーを検出するとラインレンダラーで視線を表示し、弾を複数回に分けて発射する。
/// - 弾発射は EnemyBulletPool を利用してプールから取得する。
/// </summary>
public class StarEnemy : EnemyBase
{
    // Inspector で設定する値
    [SerializeField, Header("ライン色")]
    private Color _lineColor;

    [SerializeField, Header("探索距離")]
    private float _searchDistance = 10;

    [SerializeField, Header("弾列のライン数")]
    private int _enemyBulletLineCount;

    [SerializeField, Header("弾の回数")]
    private int _enemyBulletCount;

    [SerializeField, Header("弾の回転間隔（度）」")]
    private int _enemyBulletRotation = 10;

    [SerializeField, Header("弾発射の間隔（秒）")]
    private float _enemyBulletDistance = 10;

    [SerializeField, Header("攻撃間隔（秒）")]
    private float _enemyBulletSpan = 5;

    [SerializeField, Header("弾の生成距離")]
    private int _enemyBulletInstantiateDistance = 10;

    [SerializeField]
    private EnemyBulletStatus _status;

    [SerializeField, Header("追従回転速度")]
    private float _lookatSpeed;

    [SerializeField]
    private AudioClip _alarmSound;

    [SerializeField]
    private AudioClip _deathSound;

    [SerializeField]
    private Renderer _renderer;

    // 内部状態
    private EnemyBulletPool _pool;
    private float _attackTime = 0;
    private CancellationToken _token;
    private EnemyDeadParam _deadParam = new(); 
    private bool _isAttacking;
    private LineRenderer _lineRenderer;
    private Vector3 GetTarget => TargetManager.Instance.GetPlayerPos;
    public override bool GetIsView => _renderer.isVisible;

    public override void Damage(int damage)
    {
        _enemyNowHP -= damage;
        
        if(_enemyNowHP > 0) return;

        ServiceLocator<SEManager>.GetInstance().PlaySound(_deathSound, true);
        _deadParam.enemyDeadPosition = transform.position;
        ServiceLocator<UIMediator>.GetInstance().Animation(_deadParam);
        Destroy(gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        _token = this.GetCancellationTokenOnDestroy();
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;
    }

    protected override void Start()
    {
        base.Start();
        _lineRenderer.SetPosition(0,transform.position);
        _lineRenderer.SetPosition(1, Vector3.forward * _searchDistance);
        _lineRenderer.material.color = _lineColor;
        _pool = ServiceLocator<EnemyBulletPool>.GetInstance();
        Attack().SuppressCancellationThrow().Forget();
        _deadParam.enemyDeadPosition = transform.position;
        ServiceLocator<UIMediator>.GetInstance().Init(_deadParam);
    }

    private void Update()
    {
        _attackTime += Time.deltaTime;
        if ((transform.position - GetTarget).sqrMagnitude < Mathf.Pow(_searchDistance, 2))
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(1, GetTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GetTarget - transform.position,Vector3.up),_lookatSpeed);
            Attack().SuppressCancellationThrow().Forget();
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    /// <summary>
    /// 指定範囲内に近づいたプレイヤーに扇上に弾を飛ばして攻撃する
    /// </summary>
    /// <returns></returns>
    private async UniTask Attack()
    {
        if (_isAttacking || _attackTime < _enemyBulletSpan) return;

        _isAttacking = true;
        try
        {
            _attackTime = 0;
            ServiceLocator<SEManager>.GetInstance().PlaySound(_alarmSound, true);
            for (int j = 0; j < _enemyBulletCount; j++)
            {
                for (int i = -_enemyBulletLineCount; i <= _enemyBulletLineCount; i++)
                {
                    Quaternion _rotation = transform.rotation * Quaternion.Euler(0, _enemyBulletRotation * i, 0);
                    Vector3 _pos = _rotation * Vector3.forward * _enemyBulletInstantiateDistance;
                    _pool.GetBullet(transform.position + _pos, _rotation,_status);
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_enemyBulletDistance),cancellationToken:_token);
            }
        }
        finally
        {
            _isAttacking = false;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();        
    }
}
