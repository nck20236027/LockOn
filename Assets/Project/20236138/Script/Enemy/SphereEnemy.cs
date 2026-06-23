using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// 円形（回転）攻撃を行う敵（SphereEnemy）。
/// - 一定間隔で回転しつつ弾を発射する攻撃パターンを持つ。
/// - 生成は EnemyBulletPool 経由で行う。
/// </summary>
public class SphereEnemy : EnemyBase
{
    // Inspector で設定する値
    [SerializeField, Header("コーナー数（ライン表示用）")]
    private int _cornerCount = 1;

    [SerializeField, Header("弾間隔（秒）")]
    private float _bulletTimeSpan = 1;

    [SerializeField, Header("1周あたりの弾数")]
    private int _roundBulletCount = 1;

    [SerializeField, Header("弾の最大距離")]
    private int _maxBulletDistance = 1;

    [SerializeField, Header("攻撃距離")]
    private float _distanceAttack = 2;

    [SerializeField]
    private EnemyBulletStatus _status;

    [SerializeField]
    private AudioClip _deathSound;

    // 内部状態
    private CancellationToken _cancellationToken;
    private EnemyBulletPool _pool;
    private LineRenderer _lineRenderer;
    private EnemyDeadParam _deadParam = new();
    private Renderer _renderer;

    public override bool GetIsView => _renderer.isVisible;

    private async UniTask Attack()
    {
        float _bulletCount = 0;
        while (!_cancellationToken.IsCancellationRequested)
        {
            int roundBulletCount = Mathf.Max(1, _roundBulletCount);
            _bulletCount += 360 *(_bulletTimeSpan / roundBulletCount) ;
            _bulletCount %= 360;
            Quaternion _rotation = transform.rotation * Quaternion.Euler(0,_bulletCount,0) ;
            Vector3 _vector = _rotation * Vector3.forward;
            _status.destroyTime = (_distanceAttack - _maxBulletDistance) / Mathf.Max(0.001f, _status.moveSpeed);
            _pool.GetBullet(transform.position + _vector * _maxBulletDistance, _rotation,_status);
            await UniTask.Delay(TimeSpan.FromSeconds(_bulletTimeSpan),cancellationToken:_cancellationToken);
        }
    }
    
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
        _cancellationToken = gameObject.GetCancellationTokenOnDestroy();
        _renderer = GetComponent<Renderer>();
    }

    protected override void Start()
    {
        base.Start();
        _deadParam.enemyDeadPosition = transform.position;
        ServiceLocator<UIMediator>.GetInstance().Init(_deadParam);
        _pool = ServiceLocator<EnemyBulletPool>.GetInstance();
        // キャンセルは通常終了として扱い、それ以外の例外はUniTask側のログに任せる。
        Attack().SuppressCancellationThrow().Forget();
        int cornerCount = Mathf.Max(1, _cornerCount);
        _lineRenderer.positionCount = cornerCount;
        float _rotationCircle = 360f / cornerCount;
        for (int i = 0; i < cornerCount; i++)
        {
            Quaternion _rotation = transform.rotation * Quaternion.Euler(0, _rotationCircle * i, 0);
            Vector3 _pos = transform.position + _rotation * Vector3.forward * _distanceAttack;
            _lineRenderer.SetPosition(i,_pos);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
