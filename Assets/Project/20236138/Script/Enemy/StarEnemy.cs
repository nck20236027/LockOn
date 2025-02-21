using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.WSA;

public class StarEnemy : EnemyBase
{
    [SerializeField,Header("ƒŒ[ƒU[‚ÌF")]
    private Color _lineColor;
    private LineRenderer _lineRenderer;
    [SerializeField, Header("’T’m”ÍˆÍ")]
    private float _sreachDistance = 10;
    [SerializeField, Header("o‚·’e‚Ì–{”")]
    private int _enemyBulletLineCount;
    [SerializeField, Header("‘Å‚¿o‚·’e‚Ì”")]
    private int _enemyBulletCount;
    [SerializeField, Header("’e‚Ì‘Å‚¿o‚·Šp“x")]
    private int _enemyBulletRotation = 10;
    [SerializeField, Header("’e‚ðŒ‚‚Á‚½Œã‚É‚à‚¤ˆê“x’e‚ªo‚é‚Ü‚Å")]
    private float _enemyBulletDistance = 10;
    [SerializeField, Header("ŽËŒ‚‚ÌŠÔŠu")]
    float _enemyBulletSpan = 5;
    [SerializeField,Header("’e‚ªo‚Ä‚­‚é‹——£")]
    int _enemyBulletInstatiateDistance = 10;
    [SerializeField, Header("UŒ‚—Í")]
    private int _bulletPowor = 1;
    [SerializeField, Header("’e‚ÌƒXƒs[ƒh")]
    private int _bulletSpeed = 10;
    [SerializeField, Header("U‚è‚Þ‚­‘¬‚³")]
    float _lookatSpeed;
    [SerializeField, Header("’e‚ªÁ‚¦‚é‚Ü‚Å‚Ì•b”")]
    float _bulletDestroyTime = 3;
    [SerializeField]
    GameObject _enemyBullet;
    float _attackTime = 0;

    private Vector3 GetTarget => TargetManager.Instance.GetPlayerPos;
    public override bool GetIsView => _renderer.isVisible;

    public override void Damage(int damage)
    {
        Destroy(gameObject);
    }

    private CancellationToken token;
    private Renderer _renderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        token = this.GetCancellationTokenOnDestroy();
        _renderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _lineRenderer.SetPosition(1, Vector3.forward * _sreachDistance);
        _lineRenderer.material.color = _lineColor;
        var _ = Attack();
    }

    // Update is called once per frame
    void Update()
    {
        _attackTime += Time.deltaTime;
        if ((transform.position - GetTarget).sqrMagnitude < Mathf.Pow(_sreachDistance, 2))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GetTarget - transform.position,Vector3.up),_lookatSpeed);
            var _ =  Attack();
            
        }
    }

    private async Task Attack()
    {
        if (_attackTime < _enemyBulletSpan) return;
        _attackTime = 0;
        for (int j = 0; j < _enemyBulletCount; j++)
        {
            for (int i = -_enemyBulletLineCount; i < _enemyBulletLineCount; i++)
            {
                Quaternion _rotation = transform.rotation * Quaternion.Euler(0, _enemyBulletRotation * i, 0);
                Vector3 _pos = _rotation * Vector3.forward * _enemyBulletInstatiateDistance;
                EnemyBullet bullet = Instantiate(_enemyBullet, transform.position + _pos, _rotation).GetComponent<EnemyBullet>();
                bullet.moveSpeed = _bulletSpeed;
                bullet.BulletPowor = _bulletPowor;
                Destroy(bullet.gameObject, _bulletDestroyTime);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(_enemyBulletDistance));
        }
    }
}
