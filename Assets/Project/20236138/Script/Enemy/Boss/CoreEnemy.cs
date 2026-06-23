using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

/// <summary>
/// ボス（CoreEnemy）のメイン実装クラス。
/// - 各 Act（Act1/Act2/Act3）や Idle/Death の状態を持ち、EnemyStateMachine によって行動を制御する。
/// - 弾発射や小型敵生成、HP 管理、UI 連携（HPバー等）を担当する。
/// </summary>
public class CoreEnemy : EnemyBase, ISpeaker
{
    // 共通設定
    [SerializeField, Header("ワープ位置（ランダムに飛ばす）")]
    private Transform[] _warpPos;

    [SerializeField, Header("ACT 切替の間隔（秒）")]
    private float _actionInterval = 1f;

    [SerializeField, Header("状態遷移時のアニメーション時間（秒）")]
    private float _animationTime = 2;

    [SerializeField, Header("ボスの名前")]
    private string _bossName = "コア";  

    // Act1 関連のパラメータ
    [SerializeField,Header("弾を周囲にばらまく攻撃のデータ")]
    private CoreEnemyAction1Data _action1Data;

    // Act2 関連のパラメータ
    [SerializeField,Header("プレイヤーに向かって弾を扇状に飛ばす攻撃のデータ")]
    private CoreEnemyAction2Data _action2Data;

    [SerializeField,Header("自爆する敵を生成する攻撃用データ")]
    // Act3 関連のパラメータ
    private CoreEnemyAction3Data _action3Data;

    // コンポーネント・外部参照
    [SerializeField]
    private Renderer _renderer;

    [SerializeField]
    private TriangleEnemy _enemyPrefab;

    [SerializeField]
    private AudioClip _coreStateSound;
    
    private LineRenderer _lineRenderer;

    private Animator _animator;

    // 内部状態
    private EnemyStateMachine _stateMachine;
    
    private CancellationTokenSource _damageToken;
    
    private CancellationTokenSource _token;
    
    private EnemyBulletPool _bulletPool;
    
    private BossHpBarParam _CorehpBarParam = new();
    
    private bool _isAttack = false;

    // 公開プロパティ
    public int NowEnemyHp => _enemyNowHP;

    //弾を周囲にばらまく攻撃の弾が消滅する時間を計算して提供するプロパティ
    public float Act1DestroyTime =>
        (_action1Data.Act1AttackDistance - _action1Data.DistanceAttack) / _action1Data.Act1BulletStatus.moveSpeed;
    
    public bool IsAttack { get => _isAttack; set => _isAttack = value; }
    
    public float ActionInterval => _actionInterval;
    
    public float AnimationTime => _animationTime;
    
    public CoreEnemyAction1Data CoreEnemyAction1Data => _action1Data; 

    public CoreEnemyAction2Data CoreEnemyAction2Data => _action2Data;

    public CoreEnemyAction3Data CoreEnemyAction3Data => _action3Data;

    public LineRenderer LineRenderer => _lineRenderer;

    public CancellationToken Token => _token.Token;
    
    public EnemyBulletPool BulletPool => _bulletPool;
    
    public override bool GetIsView => _renderer.isVisible;
    
    public Vector3 SpeakerPos => transform.position;
    
    public AudioClip CoreStateSound => _coreStateSound;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startColor = _action2Data.LineColor;
        _lineRenderer.endColor = _action2Data.LineColor;
        _animator = GetComponent<Animator>();
        _enemyNowHP = _enemyMaxHP;
        _stateMachine = new EnemyStateMachine(this);
        CreateDamageToken();
    }

    protected override void Start()
    {
        base.Start();
        _bulletPool = ServiceLocator<EnemyBulletPool>.GetInstance();
        _stateMachine.Initialize((int)CoreEnemyState.Act2);
        _stateMachine.OnEnter();
        _CorehpBarParam.bossName = _bossName;
        _CorehpBarParam.bossMaxHp = _enemyMaxHP;
        _CorehpBarParam.bossNowHp = _enemyMaxHP;
        ServiceLocator<UIMediator>.GetInstance().Init(_CorehpBarParam);
    }


    void Update()
    {
        _stateMachine.OnUpdate();
        CoreAnimation();
    }

    private void FixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
    }

    public void CreateTriangleEnemy(Vector3 pos,Quaternion rotation)
    {
        TriangleEnemy enemy = Instantiate(_enemyPrefab, pos, rotation);
        enemy.SetStatus(_action3Data.SelfDestructionDamage, _action3Data.EnemyDestructionTime, _action3Data.EnemyMoveStatus.MaxSpeed, _action3Data.EnemyMoveStatus, true);
    }

    private void CoreAnimation()
    {
        _animator.SetBool("isAttack", _isAttack);
    }

    /// <summary>
    /// 破棄時にキャンセルトークンを片付け、基底クラスの破棄処理を呼ぶ。
    /// </summary>
    protected override void OnDestroy()
    {
        _damageToken?.Cancel();
        DisposeDamageToken();
        base.OnDestroy();
    }

    public override void Damage(int damage)
    {
        if (!_isAttack) return;
        gameObject.transform.position = _warpPos[UnityEngine.Random.Range(0, _warpPos.Length)].position;
        _enemyNowHP -= damage;
        _CorehpBarParam.bossNowHp = _enemyNowHP;
        ResetDamageToken();
        ServiceLocator<UIMediator>.GetInstance().Animation(_CorehpBarParam);
        if (_enemyNowHP <= 0)
            _stateMachine.ChangeState((int)CoreEnemyState.Death);
    }

    /// <summary>
    /// 被弾時に現在の攻撃処理を止めるため、キャンセルトークンを作り直す。
    /// </summary>
    private void ResetDamageToken()
    {
        _damageToken?.Cancel();
        DisposeDamageToken();
        CreateDamageToken();
    }

    /// <summary>
    /// ボス用のキャンセルトークンを生成する。
    /// </summary>
    private void CreateDamageToken()
    {
        _damageToken = new CancellationTokenSource();
        _token = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy(), _damageToken.Token);
    }

    /// <summary>
    /// ボス用のキャンセルトークンを破棄する。
    /// </summary>
    private void DisposeDamageToken()
    {
        _token?.Dispose();
        _damageToken?.Dispose();
        _token = null;
        _damageToken = null;
    }
}
