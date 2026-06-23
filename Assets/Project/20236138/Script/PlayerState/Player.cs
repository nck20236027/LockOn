using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー本体のメインコンポーネント。
/// 移動・ブースト・被ダメージ・ターゲット切替などの入力と状態管理を行い、状態機械（StateMachine）を介して挙動を切り替える。
/// 他のシステム（SE, UI, Camera, TargetManager 等）とは ServiceLocator を通じて連携する。
/// </summary>
public class Player : MonoBehaviour,IMoveObjectable,IDamageable,IListener
{
    [SerializeField, Header("最大燃料量")]
    private float _maxFuelQuantity;

    [SerializeField, Range(0, 1), Header("警告閾値（最大に対する割合）")]
    private float _warningValue;

    [SerializeField, Header("無敵時間（ダメージ後の回避時間）")]
    private float _invincibleTime = 1;

    [SerializeField, Header("ブースト停止までの時間（秒）")]
    private float _boostStopTime;

    [SerializeField,Header("攻撃力（触れた相手に与えるダメージ）")]
    private int _playerPower = 1;

    [SerializeField,Header("回復できるターゲットをロックオンした際の回復値")]
    private int _lockOnEnergyRecoveryAmount;

    [SerializeField,Header("ブースト中に状態変更が許可されるまでの時間")]
    private float _boostStatusUnChangeTime;

    [SerializeField,Header("通常時の移動ステータス")]
    private PlayerMoveStatus _normalStatus;

    [SerializeField,Header("ブースト時の移動ステータス")]
    private PlayerMoveStatus _boostStatus;

    [SerializeField,Header("減速時の移動ステータス")]
    private PlayerMoveStatus _decelerationStatus;

    [SerializeField,Header("SE設定")]
    private PlayerSoundData _playerSoundData;

    [SerializeField]
    private float _standbyTime = 2;
    
    private float _fuelQuantity;


    // 外部から参照される入力・状態データ
    public PlayerAction playerInput;     

    public GenericInterfaceWrapper<ICameraControllable,CameraController> cameraController;

    public EnergyGageParam energyGaugeParam;

    public GameOverParam gameOverParam;


    [HideInInspector]
    public bool isBoostButton;

    [HideInInspector]
    public bool isDecelerationButton;


    // 内部状態
    private Rigidbody _rb;
    
    private ILockTargetable _target;
    
    private bool _isWarning;
    
    private float _nowIncibleTime;
    
    private StateMachine _stateMachine ;
    
    private bool _isInputBound;


    public ILockTargetable GetTarget => _target;
    
    public float StandbyTime => _standbyTime;
    
    public PlayerSoundData RocketSound => _playerSoundData;
    
    public float BoostStopTime => _boostStopTime;

    public float BoostStatusUnChangeTime => _boostStatusUnChangeTime;

    public int LockOnEnergyRecoveryAmount => _lockOnEnergyRecoveryAmount;

    public float FuelQuantity 
    { 
        get { return energyGaugeParam.nowEnergyGauge; }
        
        set 
        {
            energyGaugeParam.nowEnergyGauge = Mathf.Min(value, _maxFuelQuantity);
            _fuelQuantity = energyGaugeParam.nowEnergyGauge;
            if(!_isWarning && energyGaugeParam.nowEnergyGauge / energyGaugeParam .maxEnergyGauge < _warningValue)
            {
                _isWarning = true;
                ServiceLocator<SEManager>.GetInstance().PlaySound(_playerSoundData.WarningSound, true,true);
            }
            else if (energyGaugeParam.nowEnergyGauge / energyGaugeParam.maxEnergyGauge > _warningValue)
            {
                _isWarning = false;
            }
        } 
    }

    public PlayerMoveStatus NormalStatus => _normalStatus;

    public PlayerMoveStatus BoostStatus => _boostStatus;
    
    public PlayerMoveStatus DecelerationStatus => _decelerationStatus;
    

    // 位置や剛体などのインターフェース実装
    public Transform GetTransform => transform;

    public Rigidbody GetRigidbody => _rb;

    public Vector3 GetTargetVector => _target != null ? _target.GetTransform.position : Vector3.zero;

    public Vector3 ListenerPos => transform.position;

    private void Awake()
    {
        // リスナーとして登録し、ステートマシンを作成
        ServiceLocator<IListener>.Register(this);
        _stateMachine = new StateMachine(this);
    }

    void Start()
    {
        gameOverParam = new GameOverParam();
        energyGaugeParam = new EnergyGageParam();
        energyGaugeParam.buttonState = ButtonState.Non;
        energyGaugeParam.maxEnergyGauge = _maxFuelQuantity;
        _fuelQuantity = _maxFuelQuantity;
        energyGaugeParam.nowEnergyGauge = _fuelQuantity;
        energyGaugeParam.energyTimeLost = 0;
        energyGaugeParam.damageEnergyPoint = 0;

        // 入力のバインド
        playerInput = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        BindInputActions();
        _rb = GetComponent<Rigidbody>();
        _stateMachine.Initialize(ModeStateType.Move);
        _stateMachine.OnEnter();
        ServiceLocator<SEManager>.GetInstance().PlaySound(_playerSoundData.RocketFlightSound, false,true);
        ServiceLocator<UIMediator>.GetInstance().Init(energyGaugeParam);
    }

    void Update()
    {
        _stateMachine.OnUpdate();
        _nowIncibleTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
    }

    /// <summary>
    /// ターゲット変更処理：新しいターゲットを受け取り、サウンドなどの反応を行う
    /// </summary>
    /// <param name="target">あたらしく狙う敵のインターフェース</param>
    public void ChangeTarget(ILockTargetable target)
    {
        _target = target;
        if(target == null) return;
        
        if ( target.EnergyEffectType == RocketEnergyEffectType.Recover)
        {
            ServiceLocator<SEManager>.GetInstance().PlaySound(_playerSoundData.HealSound, true);
        }
        else
        {
            ServiceLocator<SEManager>.GetInstance().PlaySound(_playerSoundData.ChangeTargetSound, true);
        }
    }

    public void BoostAction(InputAction.CallbackContext callback)
    {
        isBoostButton = callback.canceled ? false : true;
    }

    public void DecelerationAction(InputAction.CallbackContext callback)
    {
        isDecelerationButton = callback.canceled ? false : true;
    }

    /// <summary>
    /// 指定量の燃料使用を試みる。
    /// 条件を満たす場合は燃料を消費せず、回復処理を行う。
    /// </summary>
    public void TryUseFuel(float amount)
    {
        float timeFuelQuantity = -amount;
        if(GetTarget != null && GetTarget.EnergyEffectType == RocketEnergyEffectType.Recover)
        timeFuelQuantity =  LockOnEnergyRecoveryAmount;

        FuelQuantity += timeFuelQuantity * Time.deltaTime;
        energyGaugeParam.energyTimeLost = -timeFuelQuantity * Time.deltaTime;
    }

    /// <summary>
    /// 入力アクションのイベントを登録する。
    /// </summary>
    private void BindInputActions()
    {
        if (playerInput == null || _isInputBound) return;

        playerInput.Player.Boost.started += BoostAction;
        playerInput.Player.Boost.canceled += BoostAction;
        playerInput.Player.Deceleration.started += DecelerationAction;
        playerInput.Player.Deceleration.canceled += DecelerationAction;
        _isInputBound = true;
    }

    /// <summary>
    /// 登録済みの入力イベントを解除し、破棄済みのPlayerへ通知が残らないようにする。
    /// </summary>
    private void UnbindInputActions()
    {
        if (playerInput == null || !_isInputBound) return;

        playerInput.Player.Boost.started -= BoostAction;
        playerInput.Player.Boost.canceled -= BoostAction;
        playerInput.Player.Deceleration.started -= DecelerationAction;
        playerInput.Player.Deceleration.canceled -= DecelerationAction;
        _isInputBound = false;
    }

    /// <summary>
    /// ダメージ処理（無敵時間の管理、UI更新、サウンド、カメラシェイク）
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if (_nowIncibleTime >= 0) return;
        ServiceLocator<SEManager>.GetInstance().PlaySound(_playerSoundData.DamageSound,true);
        _nowIncibleTime = _invincibleTime;
        FuelQuantity -= damage;
        energyGaugeParam.isDamage = true;
        energyGaugeParam.damageEnergyPoint = damage;
        ServiceLocator<UIMediator>.GetInstance().Animation(energyGaugeParam);
        energyGaugeParam.isDamage= false;
        cameraController.Interface.CameraShake();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable  = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.Damage(_playerPower);
        }
    }

    private void OnDestroy()
    {
        // 終了時の片付け：サウンド停止とリスナー解除
        UnbindInputActions();
        ServiceLocator<SEManager>.GetInstance().StopSound(_playerSoundData.RocketFlightSound);
        ServiceLocator<IListener>.RemoveInstance(this);
    }
}
