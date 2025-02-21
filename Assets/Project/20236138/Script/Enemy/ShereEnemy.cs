using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShereEnemy : EnemyBase
{
    [SerializeField]
    private int _cornerCount = 1;
    CancellationToken cancellationToken;
    [SerializeField, Header("íeÇ™âΩïbÇ®Ç´Ç…èoÇƒÇ≠ÇÈÇ©")]
    private float _bulletTimeSpan = 1;
    [SerializeField,Header("àÍé¸âΩïbÇ©")]
    private int _roundBulletCount = 1;
    [SerializeField,Header("íeÇ™èoÇÈãóó£")]
    private int _maxBulletDistance = 1;
    [SerializeField, Header("çUåÇîÕàÕ")]
    private float _distanceAttack = 2;
    [SerializeField,Header("íeÇÃë¨Ç≥")]
    private float _bulletSpeed = 2;
    [SerializeField, Header("çUåÇóÕ")]
    private int _bulletPowor = 1;
    private float DestroyTime => (_distanceAttack - _maxBulletDistance) / _bulletSpeed; 
    [SerializeField]
    GameObject _bullet;

    private LineRenderer _lineRenderer;
    private async UniTask Attack()
    {
        float _bulletCount = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            _bulletCountÅ@+= 360 *(_bulletTimeSpan / _roundBulletCount) ;
            _bulletCount %= 360;
            Quaternion _rotation = transform.rotation * Quaternion.Euler(0,_bulletCount,0) ;
            Vector3 _vector = _rotation * Vector3.forward;
            EnemyBullet bullet = Instantiate(_bullet, transform.position + _vector * _maxBulletDistance, _rotation).GetComponent<EnemyBullet>();
            bullet.moveSpeed = _bulletSpeed;
            bullet.BulletPowor = _bulletPowor;
            Destroy(bullet.gameObject, DestroyTime);
            await UniTask.Delay(TimeSpan.FromSeconds(_bulletTimeSpan),cancellationToken:cancellationToken);

        }
    }
    public override bool GetIsView => _renderer.isVisible;

    Renderer _renderer;
    public override void Damage(int damage)
    {
        Destroy(this.gameObject);
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
