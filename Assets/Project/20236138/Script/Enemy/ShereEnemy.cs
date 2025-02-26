using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShereEnemy : EnemyBase
{
    [SerializeField,Header("UŒ‚”ÍˆÍ‚Ì•\Ž¦‚É‰½‚©Š‚ÌŠp‚ðì‚é‚©")]
    private int _cornerCount = 1;
    CancellationToken cancellationToken;
    [SerializeField, Header("’e‚ª‰½•b‚¨‚«‚Éo‚Ä‚­‚é‚©")]
    private float _bulletTimeSpan = 1;
    [SerializeField,Header("ˆêŽü‰½•b‚©")]
    private int _roundBulletCount = 1;
    [SerializeField,Header("’e‚ªo‚é‹——£")]
    private int _maxBulletDistance = 1;
    [SerializeField, Header("UŒ‚”ÍˆÍ")]
    private float _distanceAttack = 2;
    [SerializeField]
    private EnemyBulletStatus _status;
    private float DestroyTime => (_distanceAttack - _maxBulletDistance) / _status._moveSpeed; 
    private EnemyBulletPool _pool;

    private LineRenderer _lineRenderer;
    private async UniTask Attack()
    {
        float _bulletCount = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            _bulletCount@+= 360 *(_bulletTimeSpan / _roundBulletCount) ;
            _bulletCount %= 360;
            Quaternion _rotation = transform.rotation * Quaternion.Euler(0,_bulletCount,0) ;
            Vector3 _vector = _rotation * Vector3.forward;
            _status._DestroyTime = DestroyTime;
            _pool.GetBullet(transform.position + _vector * _maxBulletDistance, _rotation,_status);
            await UniTask.Delay(TimeSpan.FromSeconds(_bulletTimeSpan),cancellationToken:cancellationToken);

        }
    }
    public override bool GetIsView => _renderer.isVisible;

    Renderer _renderer;
    public override void Damage(int damage)
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        cancellationToken = gameObject.GetCancellationTokenOnDestroy();
        _renderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _pool = ServiceLocator<EnemyBulletPool>.GetInstance();
        var _ = Attack();
        _lineRenderer.positionCount = _cornerCount;
        float _rotationCircle = 360 / _cornerCount;
        for (int i = 0; i < _cornerCount; i++)
        {
            Quaternion _rotation = transform.rotation * Quaternion.Euler(0, _rotationCircle * i, 0);
            Vector3 _pos = _rotation * Vector3.forward * _distanceAttack;
            _lineRenderer.SetPosition(i,_pos);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
