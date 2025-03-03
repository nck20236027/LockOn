using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;


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
    [SerializeField]
    private EnemyBulletStatus status;
    [SerializeField, Header("U‚è‚Þ‚­‘¬‚³")]
    float _lookatSpeed;
    EnemyBulletPool _pool;
    float _attackTime = 0;
    [SerializeField]
    AudioClip _allermSound;
    [SerializeField]
    AudioClip _deathSound;
    private Vector3 GetTarget => TargetManager.Instance.GetPlayerPos;
    public override bool GetIsView => _renderer.isVisible;

    public override void Damage(int damage)
    {
        ServiceLocator<SEManager>.GetInstance().PlaySound(_deathSound, true);
        Destroy(gameObject);
    }

    private CancellationToken token;
    [SerializeField]
    private Renderer _renderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        token = this.GetCancellationTokenOnDestroy();
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _lineRenderer.SetPosition(0,transform.position);
        _lineRenderer.SetPosition(1, Vector3.forward * _sreachDistance);
        _lineRenderer.material.color = _lineColor;
        var _ = Attack();
        _pool = ServiceLocator<EnemyBulletPool>.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        _attackTime += Time.deltaTime;
        if ((transform.position - GetTarget).sqrMagnitude < Mathf.Pow(_sreachDistance, 2))
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(1, GetTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GetTarget - transform.position,Vector3.up),_lookatSpeed);
            var _ =  Attack();
            
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    private async UniTask Attack()
    {
        if (_attackTime < _enemyBulletSpan) return;
        _attackTime = 0;
        ServiceLocator<SEManager>.GetInstance().PlaySound(_allermSound, true);
        for (int j = 0; j <= _enemyBulletCount; j++)
        {
            for (int i = -_enemyBulletLineCount; i <= _enemyBulletLineCount; i++)
            {
                Quaternion _rotation = transform.rotation * Quaternion.Euler(0, _enemyBulletRotation * i, 0);
                Vector3 _pos = _rotation * Vector3.forward * _enemyBulletInstatiateDistance;
                _pool.GetBullet(transform.position + _pos, _rotation,status);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(_enemyBulletDistance),cancellationToken:token);
        }
    }
}
