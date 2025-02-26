using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoreEnemy : EnemyBase,ILockTargetable
{
    [SerializeField, Header("ƒ{ƒX‚ÌHP(“ËŒ‚‰ñ”)")]
    private int _MaxCoreHp = 3;
    public int MaxCoreHp => _MaxCoreHp;
    private int _nowCoreHp = 0;
    public int nowCoreHp => _nowCoreHp;
    [SerializeField, Header("Act‚Ìs“®ŠÔŠu")]
    private float _actionInterval = 1f;
    public float ActionIntarval => _actionInterval;
    [SerializeField, Header("Act1‚ÌUŒ‚ŽžŠÔ")]
    private float _act1AttackTime = 1f;
    public float Act1AttackTime => _act1AttackTime;
    [SerializeField, Header("Act1‚ÌŒ„‚ð‚³‚ç‚·ŽžŠÔ")]
    private float _act1ChanseTime = 1f;
    public float Act1ChanseTime => _act1ChanseTime;
    [SerializeField, Header("’e‚Ì¶¬‚ÌŠÔŠu")]
    private float _act1CreatBulletInterval = 1;
    public float Act1CreatBulletIntarval => _act1CreatBulletInterval;
    [SerializeField, Header("UŒ‚”ÍˆÍ‚Ì”¼Œa")]
    private float _act1AttackDistanse = 20;
    public float Act1AttackDistanse => _act1AttackDistanse;
    [SerializeField]
    EnemyBulletStatus _act1BulletStatus;
    public EnemyBulletStatus Act1BulletStatus => _act1BulletStatus;

    [SerializeField, Header("Act2‚ÌUŒ‚ŽžŠÔ")]
    private float _act2AttackTime = 1f;
    public float Act2AttackTime => _act2AttackTime;
    [SerializeField, Header("Act2‚ÌŒ„‚ð‚³‚ç‚·ŽžŠÔ")]
    private float _act2ChanseTime = 1f;
    public float Act2ChanseTime => _act2ChanseTime;
    [SerializeField, Header("Š´’m”ÍˆÍ")]
    private float _sreachDistance = 10;
    public float SreachDistance => _sreachDistance;
    [SerializeField, Header("’e‚Ì‘Å‚¿o‚·”iWayj")]
    private int _act2BulletLineCount = 2;
    public int Act2BulletLineCount => _act2BulletLineCount;
    [SerializeField, Header("‘Å‚¿o‚·’e‚Ì”")]
    private int _act2BulletCount;
    public int Act2BulletCount => _act2BulletCount;
    [SerializeField, Header("’e‚Ì‘Å‚¿o‚·Šp“x")]
    private int _enemyBulletRotation = 10;
    public int AnemyBulletRotation => _enemyBulletRotation;
    [SerializeField, Header("’e‚ðŒ‚‚Á‚½Œã‚É‚à‚¤ˆê“x’e‚ªo‚é‚Ü‚Å")]
    private float _enemyBulletDistance = 10;
    public float EnemyBulletDistance => _enemyBulletDistance;
    [SerializeField, Header("ŽËŒ‚‚ÌŠÔŠu")]
    private float _enemyBulletSpan = 5;
    public float EnemyBulletSpan => _enemyBulletSpan;
    [SerializeField, Header("’e‚ªo‚Ä‚­‚é‹——£")]
    private int _enemyBulletInstatiateDistance = 10;
    public int EnemyBulletInstatiateDistance => _enemyBulletInstatiateDistance;
    [SerializeField, Header("ƒŒ[ƒU[‚ÌF")]
    private Color _lineColor; [SerializeField]
    public Color LineColor => _lineColor;
    private EnemyBulletStatus status;
    public EnemyBulletStatus Status => status;


    [SerializeField, Header("Act3‚ÌUŒ‚ŽžŠÔ")]
    private float _act3AttackTime = 1f;
    public float Act3AttackTime => _act3AttackTime;
    [SerializeField, Header("Act3“G‚Ì¶¬‚ÌŠÔŠu")]
    private float _act3CrealEnemyInterval = 1;
    public float Act3CreatEnemyInterval => _act3CrealEnemyInterval;
    [SerializeField, Header("Act3‚ÌŒ„‚ð‚³‚ç‚·ŽžŠÔ")]
    private float _act3ChanseTime = 1f;
    public float Act3ChanseTime => _act3ChanseTime;
    [SerializeField,Header("act3‚Ì“G‚ÌƒXƒe[ƒ^ƒX")]
    private MoveStatus _moveStatus;
    public MoveStatus MoveStatus => _moveStatus;
    [SerializeField,Header("“G‚ªŽ©”š‚·‚é‚Ü‚Å‚ÌŽžŠÔ")]
    private float _enemyDestructionTime = 1;
    public float EnemyDestructionTime => _enemyDestructionTime;
    [SerializeField,Header("“G‚ªŽ©”š‚µ‚½Žž‚Ìƒ_ƒ[ƒW")]
    private float _selfDistructionDamage = 10;
    public float SelfDistructionDamage => _selfDistructionDamage;
    private Renderer _renderer;
    
    //UŒ‚’†‚©‚Ç‚¤‚©
    private bool _isAttack = false;

    EnemyStateMachine _stateMachin;

    public override bool GetIsView => _renderer.isVisible;

    public override void Damage(int damage)
    {
        if (_isAttack) return;
        _MaxCoreHp -= damage;
        if (_MaxCoreHp < 0)
            Debug.Log("Boss is Dead");
    }


    private EnemyBulletPool _bulletPool;
    private void Awake()
    {
        _nowCoreHp = _MaxCoreHp;
        _stateMachin = new EnemyStateMachine(this);

        _renderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _bulletPool = ServiceLocator<EnemyBulletPool>.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
