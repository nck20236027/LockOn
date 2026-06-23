using UnityEngine;

/// <summary>
/// CoreEnemyが自爆する敵を複数乗艦する際に必要なデータ
/// </summary>
[CreateAssetMenu(menuName = "Data/Enemy/CoreAction3Data", fileName = "CoreEnemyAction3Data",order = 3)]
public class CoreEnemyAction3Data : ScriptableObject
{
    [SerializeField, Header("Act3 の攻撃継続時間（秒）")]
    private float _act3AttackTime = 1f;

    [SerializeField, Header("Act3 の小型生成間隔（秒）")]
    private float _act3CreateEnemyInterval = 1;

    [SerializeField, Header("Act3 の待機時間（秒）")]
    private float _act3ChanceTime = 1f;

    [SerializeField, Header("小型生成の距離閾値")]
    private float _act3EnemyCreateDistance = 2;

    [SerializeField, Header("移動ステータス（Act3 用）")]
    private MoveStatus _enemyMoveStatus;

    [SerializeField, Header("小型生成の破壊時間（秒）")]
    private float _enemyDestructionTime = 1;

    [SerializeField, Header("自爆ダメージ（小型）")]
    private int _selfDestructionDamage = 10;
    
    public float Act3AttackTime => _act3AttackTime;

    public float Act3CreateEnemyInterval => _act3CreateEnemyInterval;

    public float Act3ChanceTime => _act3ChanceTime;

    public float Act3EnemyCreateDistance => _act3EnemyCreateDistance;

    public MoveStatus EnemyMoveStatus => _enemyMoveStatus;

    public float EnemyDestructionTime => _enemyDestructionTime;

    public int SelfDestructionDamage => _selfDestructionDamage;
}
