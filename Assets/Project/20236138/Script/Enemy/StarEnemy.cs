using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class StarEnemy : EnemyBase
{
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

    private Vector3 GetTarget => TargetManager.Instance.GetPlayerPos;
    public override bool GetIsView => renderer.isVisible;

    public override void Damage(int damage)
    {
        Destroy(gameObject);
    }

    private CancellationToken token;
    private Renderer renderer;

    private void Awake()
    {
        token = this.GetCancellationTokenOnDestroy();
        renderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Attack();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private async UniTask Attack()
    {
        while (!token.IsCancellationRequested)
        {
            if ((transform.position - GetTarget).sqrMagnitude < Mathf.Pow(_sreachDistance, 2))
            {

                for (int j = 0; j < _enemyBulletCount; j++)
                {
                    for (int i = -_enemyBulletLineCount; i < _enemyBulletLineCount; i++)
                    {
                        Quaternion _rotation = transform.rotation * Quaternion.Euler(0, _enemyBulletRotation * i, 0);
                        Vector3 _pos = _rotation * Vector3.forward * _enemyBulletInstatiateDistance;
                        EnemyBullet bullet = Instantiate(_enemyBullet, transform.position + _pos, _rotation).GetComponent<EnemyBullet>();
                        bullet.moveSpeed = _bulletSpeed;
                        bullet.BulletPowor = _bulletPowor;
                        Destroy(bullet.gameObject);
                    }
                    await UniTask.Delay(TimeSpan.FromSeconds(_enemyBulletDistance));
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_enemyBulletSpan));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(GetTarget, transform.position),_lookatSpeed);
            }
            await UniTask.Delay(0);
        }
    }
}
