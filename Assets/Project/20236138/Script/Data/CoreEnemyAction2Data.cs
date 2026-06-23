using UnityEngine;

/// <summary>
/// CoreEnemyがプレイヤーに向かって弾を扇上に飛ばす際に必要なデータ
/// </summary>
[CreateAssetMenu(menuName = "Data/Enemy/CoreAction2Data", fileName = "CoreEnemyAction2Data",order = 2)]
public class CoreEnemyAction2Data : ScriptableObject
{
    [SerializeField, Header("Act2 の攻撃継続時間（秒）")]
    private float _act2AttackTime = 1f;

    [SerializeField, Header("Act2 の待機時間（秒）")]
    private float _act2ChanceTime = 1f;

    [SerializeField, Header("探索距離（プレイヤーを検出する半径）")]
    private float _searchDistance = 10;

    [SerializeField, Header("Act2 の弾列ライン数（左右方向の列数）")]
    private int _act2BulletLineCount = 2;

    [SerializeField, Header("Act2 の弾の回数（バースト回数）")]
    private int _act2BulletCount;

    [SerializeField, Header("弾回転間隔（度）")]
    private int _enemyBulletRotation = 10;

    [SerializeField, Header("弾の発射間隔（秒）")]
    private float _enemyBulletDistance = 10;

    [SerializeField, Header("弾バーストの間隔（秒）")]
    private float _enemyBulletSpan = 5;

    [SerializeField, Header("弾生成の距離（前方オフセット）")]
    private int _enemyBulletInstantiateDistance = 10;

    [SerializeField, Header("ライン表示色")]
    private Color _lineColor;

    [SerializeField, Header("Act2 の弾パラメータ")]
    private EnemyBulletStatus _act2BulletStatus;

    [SerializeField, Header("Act2の振り向き速度")]
    private float _act2RotationSpeed = 1f;

    public float Act2AttackTime => _act2AttackTime;
    
    public float Act2ChanceTime => _act2ChanceTime;
    
    public float SearchDistance => _searchDistance;
    
    public int Act2BulletLineCount => _act2BulletLineCount;
    
    public int Act2BulletCount => _act2BulletCount;
    
    public int EnemyBulletRotation => _enemyBulletRotation;
    
    public float EnemyBulletDistance => _enemyBulletDistance;
    
    public float EnemyBulletSpan => _enemyBulletSpan;
    
    public int EnemyBulletInstantiateDistance => _enemyBulletInstantiateDistance;
    
    public EnemyBulletStatus Act2BulletStatus => _act2BulletStatus;
    
    public Color LineColor => _lineColor;
    
    public float Act2RotationSpeed => _act2RotationSpeed;
}
