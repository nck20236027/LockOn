using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour,IMoveObjectable,IDamagable
{
    public PlayerAction _playerInput ; 
    //コンポーネント
    [HideInInspector]
    private Rigidbody _rb;
    public GenericInterfaceWrapper<ICameraContollorable,CameraController> _cameraController;

    //ターゲットのオブジェクト
    private ILockTargetable _target;
    public ILockTargetable GetTarget => _target;

    [SerializeField]
    private float _standbyTime = 2;
    public float StandbyTime => _standbyTime;

    //playerのステータス
    [SerializeField, Header("最大の燃料量")]
    float _maxFuelQuantity;
    [Header("現在の燃料量")]
    public float _fuelQuantity;
    public float FuelQuantity { get { return _energyGageParam.nowEnergyGauge; }
        set { _energyGageParam.nowEnergyGauge = Mathf.Min(value, _maxFuelQuantity);
            _fuelQuantity = _energyGageParam.nowEnergyGauge;
        } }
    [SerializeField, Header("ダメージを受けた時の無敵時間")]
    private float _invincibleTime = 1;
    private float _nowIncibleTime = 0;
    [Header("ブーストが開始したとき~切り替えれない時間")]
    public float BoostStateUnChangeTime;
    [ Header("通常のステータス")]
    public PlayerMoveStatus normalState;
    [Header("ブーストのステータス")]
    public PlayerMoveStatus BoostState;
    [Header("減速のステータス")]
    public PlayerMoveStatus decelerationState;


    [HideInInspector]
    public bool isBoostButton = false;
    [HideInInspector]
    public bool isDecelerationButton = false;
    private StateMachine _stateMachine ;

    public Transform GetPos => transform;

    public Rigidbody GetRigidbody => _rb;

    public Vector3 Gettarget =>_target != null ? _target.GetTokenPosition : Vector3.zero;

    public EnergyGageParam _energyGageParam;

    private void Awake()
    {
        _stateMachine = new StateMachine(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        _energyGageParam = new EnergyGageParam();
        _energyGageParam.buttonState = ButtonState.Non;
        _energyGageParam.maxEnergyGauge = _maxFuelQuantity;
        _energyGageParam.nowEnergyGauge = _fuelQuantity;
        _fuelQuantity = _maxFuelQuantity;
        _energyGageParam.energyTimeLost = 0;
        _energyGageParam.damageEnergyPoint = 0;

        _playerInput = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        _playerInput.Player.Boost.started += BoostAction;
        _playerInput.Player.Boost.canceled += BoostAction;
        _playerInput.Player.Deceleration.started += DecelerationAction;
        _playerInput.Player.Deceleration.canceled += DecelerationAction;
        _rb = GetComponent<Rigidbody>();
        _stateMachine.Initialize(ModeStateType.Move);
        _stateMachine.OnEnter();

        ServiceLocator<UIMediator>.GetInstance().Init(_energyGageParam);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.OnUpdate();
        _nowIncibleTime -= Time.deltaTime;




    }
    private void FixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
        //Vector3 lef = targetPos != null ? targetPos.position - transform.position : transform.up;

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, lef), normalState.Bendability);
        
        //rb.AddForce(transform.up * normalState.MaxSpeed);
        //if(rb.velocity.sqrMagnitude > Mathf.Pow(normalState.MaxSpeed, 2))
        //    {
        //    rb.velocity = rb.velocity / rb.velocity.magnitude * normalState.MaxSpeed;
        //}
    }


    //プレイヤーの移動
    private void PlayerMove()
    {

        //旧式の移動方法
        //Vector3 lef = targetPos.position - transform.position;

        //transform.rotation =
        //    Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lef, Vector3.up), nowState.Bendability);
        //rb.velocity = transform.rotation * Vector3.forward * playerMoveSpeed;

    }


    //playerのターゲットを変えるときに呼ぶ
    public void ChangeTarget(ILockTargetable _target)
    {
        this._target = _target;
    }

    public void BoostAction(InputAction.CallbackContext callback)
    {
        isBoostButton = callback.canceled ? false : true;
    }

    public void DecelerationAction(InputAction.CallbackContext callback)
    {
        isDecelerationButton = callback.canceled ? false : true;
    }

    public void Damage(int damage)
    {
        if (_nowIncibleTime >= 0) return;
        _nowIncibleTime = _invincibleTime;
        FuelQuantity -= damage;
        _energyGageParam.isDamage = true;
        _energyGageParam.damageEnergyPoint = damage;
        ServiceLocator<UIMediator>.GetInstance().Animation(_energyGageParam);
        _energyGageParam.isDamage= false;
        _cameraController.Interface.CameraSheikh();

    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable  = other.GetComponent<IDamagable>();
        if(damagable != null)
        {
            damagable.Damage(1);
        }
    }
}


