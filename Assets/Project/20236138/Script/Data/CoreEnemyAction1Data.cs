using UnityEngine;

/// <summary>
/// CoreEnemyがコアを中心として円上に弾を飛ばして攻撃する際に必要なデータ
/// </summary>
[CreateAssetMenu(menuName = "Data/Enemy/CoreAction1Data", fileName = "CoreEnemyAction1Data",order = 1)]
public class CoreEnemyAction1Data : ScriptableObject
{
    [SerializeField, Header("Act1 の攻撃継続時間（秒）")]
    private float _act1AttackTime = 1f;

    [SerializeField, Header("Act1 の待機時間（秒）")]
    private float _act1ChanceTime = 1f;

    [SerializeField, Header("Act1 の弾生成間隔（秒）")]
    private float _act1CreateBulletInterval = 1;

    [SerializeField, Header("Act1 の攻撃距離")]
    private float _act1AttackDistance = 20;

    [SerializeField, Header("弾発射の基準距離（短距離）")]
    private float _distanceAttack = 2;

    [SerializeField, Header("弾の広がり（角度等の基準）")]
    private float _bulletAround = 3;

    [SerializeField]
    private EnemyBulletStatus _act1BulletStatus;

    public float Act1AttackTime => _act1AttackTime;
    
    public float Act1ChanceTime => _act1ChanceTime;
    
    public float Act1CreateBulletInterval => _act1CreateBulletInterval;
    
    public float Act1AttackDistance => _act1AttackDistance;
    
    public float DistanceAttack => _distanceAttack;
    
    public float BulletAround => _bulletAround;
    
    public EnemyBulletStatus Act1BulletStatus => _act1BulletStatus;
}
