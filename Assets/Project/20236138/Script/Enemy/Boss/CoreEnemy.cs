using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class CoreEnemy : EnemyBase,ILockTargetable, ISpeaker
{
    [SerializeField, Header("É{ÉXÇÃHP(ìÀåÇâÒêî)")]
    private int _MaxCoreHp = 3;
    public int MaxCoreHp => _MaxCoreHp;
    private int _nowCoreHp = 0;
    public int nowCoreHp => _nowCoreHp;
    [SerializeField, Header("ActÇÃçsìÆä‘äu")]
    private float _actionInterval = 1f;
    public float ActionIntarval => _actionInterval;
    [SerializeField, Header("ÉAÉjÉÅÅ[ÉVÉáÉìÇ™à»ç~Ç∑ÇÈéûä‘")]
    private float _animationTime = 2;
    public float AnimationTime => _animationTime;
    [SerializeField, Header("Act1ÇÃçUåÇéûä‘")]
    private float _act1AttackTime = 1f;
    public float Act1AttackTime => _act1AttackTime;
    [SerializeField, Header("Act1ÇÃåÑÇÇ≥ÇÁÇ∑éûä‘")]
    private float _act1ChanseTime = 1f;
    public float Act1ChanseTime => _act1ChanseTime;
    [SerializeField, Header("íeÇÃê∂ê¨ÇÃä‘äu")]
    private float _act1CreatBulletInterval = 1;
    public float Act1CreatBulletIntarval => _act1CreatBulletInterval;
    [SerializeField, Header("çUåÇîÕàÕÇÃîºåa")]
    private float _act1AttackDistanse = 20;
    public float Act1AttackDistanse => _act1AttackDistanse;
    [SerializeField,Header("íeÇ™ê∂ê¨Ç≥ÇÍÇÈãóó£")]
    private float _distanceAttack = 2;
    public float DistanceAttack => _distanceAttack;
    [SerializeField, Header("íeÇ™àÍé¸âΩïbÇ≈èIÇÌÇËÇ©")]
    private float _bulletAround = 3;
    public float BulletAround => _bulletAround;
    public float Act1DestroyTime =>
        (_act1AttackDistanse - _distanceAttack) / _act1BulletStatus._moveSpeed;
    [SerializeField]
    EnemyBulletStatus _act1BulletStatus;
    public EnemyBulletStatus Act1BulletStatus => _act1BulletStatus;

    [SerializeField, Header("Act2ÇÃçUåÇéûä‘")]
    private float _act2AttackTime = 1f;
    public float Act2AttackTime => _act2AttackTime;
    [SerializeField, Header("Act2ÇÃåÑÇÇ≥ÇÁÇ∑éûä‘")]
    private float _act2ChanseTime = 1f;
    public float Act2ChanseTime => _act2ChanseTime;
    [SerializeField, Header("ä¥ímîÕàÕ")]
    private float _sreachDistance = 10;
    public float SreachDistance => _sreachDistance;
    [SerializeField, Header("íeÇÃë≈ÇøèoÇ∑êîÅiWayÅj")]
    private int _act2BulletLineCount = 2;
    public int Act2BulletLineCount => _act2BulletLineCount;
    [SerializeField, Header("ë≈ÇøèoÇ∑íeÇÃêî")]
    private int _act2BulletCount;
    public int Act2BulletCount => _act2BulletCount;
    [SerializeField, Header("íeÇÃë≈ÇøèoÇ∑äpìx")]
    private int _enemyBulletRotation = 10;
    public int AnemyBulletRotation => _enemyBulletRotation;
    [SerializeField, Header("íeÇåÇÇ¡ÇΩå„Ç…Ç‡Ç§àÍìxíeÇ™èoÇÈÇ‹Ç≈")]
    private float _enemyBulletDistance = 10;
    public float EnemyBulletDistance => _enemyBulletDistance;
    [SerializeField, Header("éÀåÇÇÃä‘äu")]
    private float _enemyBulletSpan = 5;
    public float EnemyBulletSpan => _enemyBulletSpan;
    [SerializeField, Header("íeÇ™èoÇƒÇ≠ÇÈãóó£")]
    private int _enemyBulletInstatiateDistance = 10;
    public int EnemyBulletInstatiateDistance => _enemyBulletInstatiateDistance;
    [SerializeField, Header("ÉåÅ[ÉUÅ[ÇÃêF")]
    private Color _lineColor; [SerializeField]
    public Color LineColor => _lineColor;
    [SerializeField,Header("Act2ÇÃíeÇÃÉXÉeÅ[É^ÉX")]
    private EnemyBulletStatus _act2Bulletstatus;
    public EnemyBulletStatus Act2BulletStatus => _act2Bulletstatus;


    [SerializeField, Header("Act3ÇÃçUåÇéûä‘")]
    private float _act3AttackTime = 1f;
    public float Act3AttackTime => _act3AttackTime;
    [SerializeField, Header("Act3ìGÇÃê∂ê¨ÇÃä‘äu")]
    private float _act3CrealEnemyInterval = 1;
    public float Act3CreatEnemyInterval => _act3CrealEnemyInterval;
    [SerializeField, Header("Act3ÇÃåÑÇÇ≥ÇÁÇ∑éûä‘")]
    private float _act3ChanseTime = 1f;
    public float Act3ChanseTime => _act3ChanseTime;
    [SerializeField, Header("ìGÇÇ«ÇÍÇ≠ÇÁÇ¢ÇÕÇ»ÇµÇƒê∂ê¨Ç∑ÇÈÇ©")]
    private float _act3EnemCreatDistance = 2;
    public float Act3EnemyCreatDistance => _act3EnemCreatDistance;
    [SerializeField,Header("act3ÇÃìGÇÃÉXÉeÅ[É^ÉX")]
    private MoveStatus _moveStatus;
    public MoveStatus MoveStatus => _moveStatus;
    [SerializeField,Header("ìGÇ™é©îöÇ∑ÇÈÇ‹Ç≈ÇÃéûä‘")]
    private float _enemyDestructionTime = 1;
    public float EnemyDestructionTime => _enemyDestructionTime;
    [SerializeField,Header("ìGÇ™é©îöÇµÇΩéûÇÃÉ_ÉÅÅ[ÉW")]
    private int _selfDistructionDamage = 10;
    public int SelfDistructionDamage => _selfDistructionDamage;
    [SerializeField]
    private Renderer _renderer;

    LineRenderer _lineRenderer;
    public LineRenderer LineRenderer => _lineRenderer;

    [SerializeField]
    Transform _player;
    public Vector3 GetPlayerPos  => _player.position;

    [SerializeField]
    TriangleEnemy _enemy;

    Animator _animator;

    [SerializeField] private Vector3 corePos;
    public Vector3 SpeakerPos => corePos;
    public AudioClip coreStateSound;

    //çUåÇíÜÇ©Ç«Ç§Ç©
    [SerializeField]
    private bool _isAttack = false;
    public bool IsAttack { get => _isAttack; set => _isAttack = value; }

    private EnemyStateMachine _stateMachin;
    private CancellationTokenSource _damageToken;
    private CancellationTokenSource _token;
    public CancellationToken Token => _token.Token;
    private EnemyBulletPool _bulletPool;
    public EnemyBulletPool BulletPool => _bulletPool;
    public override bool GetIsView => _renderer.isVisible;


    private BossHpBarParam _CorehpBarParam = new();
    public override void Damage(int damage)
    {
        if (!_isAttack) return;
        _nowCoreHp -= damage;
        _CorehpBarParam.bossNowHp = _nowCoreHp;
        _damageToken.Cancel();
        _damageToken = new CancellationTokenSource();
        _token = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy(), _damageToken.Token);
        ServiceLocator<UIMediator>.GetInstance().Animation(_CorehpBarParam);
        if (_nowCoreHp <= 0)
            _stateMachin.ChangeState((int)CoreEnemyState.Death);
    }


    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;
        _animator = GetComponent<Animator>();
        _nowCoreHp = _MaxCoreHp;
        _stateMachin = new EnemyStateMachine(this);
        _damageToken = new CancellationTokenSource();
        _token = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy(),_damageToken.Token);

    }
    // Start is called before the first frame update
    protected override void Start()
    {
        //base.Start();
        _bulletPool = ServiceLocator<EnemyBulletPool>.GetInstance();
        _stateMachin.Initialize((int)CoreEnemyState.Act2);
        _stateMachin.OnEnter();
        _CorehpBarParam.bossName = "ÉRÉA";
        _CorehpBarParam.bossMaxHp = _MaxCoreHp;
        _CorehpBarParam.bossNowHp = _MaxCoreHp;
        ServiceLocator<UIMediator>.GetInstance().Init(_CorehpBarParam);
        corePos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachin.OnUpdate();
        CoreAnimation();
    }

    private void FixedUpdate()
    {
        _stateMachin.OnFixedUpdate();
    }

    public void CreatTriangleEnemy(Vector3 _pos,Quaternion _rotation)
    {
        TriangleEnemy enemy = Instantiate(_enemy, _pos, _rotation);
        enemy.SetStatus(SelfDistructionDamage, EnemyDestructionTime, MoveStatus.MaxSpeed, MoveStatus, true);
    }

    private void CoreAnimation()
    {
        _animator.SetBool("isAttack", _isAttack);
    }
}
