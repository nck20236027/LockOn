using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour,IMoveObjectable
{
    public PlayerAction _playerInput ; 
    //コンポーネント
    [HideInInspector]
    private Rigidbody _rb;
    public GenericInterfaceWrapper<ICameraContollorable,CameraController> cameraController;

    //ターゲットのオブジェクト
    ILockTargetable _target;

    //playerのステータス
    [SerializeField, Header("最大の燃料量")]
    float _maxFuelQuantity;
    [Header("現在の燃料量")]
    public float fuelQuantity;
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

    public ILockTargetable Gettarget =>_target;

    private void Awake()
    {
        _stateMachine = new StateMachine(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = new();
        _playerInput.Enable();
        _playerInput.Player.Boost.started += BoostAction;
        _playerInput.Player.Boost.canceled += BoostAction;
        _playerInput.Player.Deceleration.canceled += DecelerationAction;
        _rb = GetComponent<Rigidbody>();
        _stateMachine.Initialize(ModeStateType.Move);
    }

    void OnDestroy()
    {
        _playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.OnUpdate();
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

}


