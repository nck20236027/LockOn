using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour,IMoveObjectable
{
    public PlayerAction _playerInput ; 
    //�R���|�[�l���g
    [HideInInspector]
    private Rigidbody _rb;
    public GenericInterfaceWrapper<ICameraContollorable,CameraController> cameraController;

    //�^�[�Q�b�g�̃I�u�W�F�N�g
    ILockTargetable _target;

    //player�̃X�e�[�^�X
    [SerializeField, Header("�ő�̔R����")]
    float _maxFuelQuantity;
    [Header("���݂̔R����")]
    public float fuelQuantity;
    [Header("�u�[�X�g���J�n�����Ƃ�~�؂�ւ���Ȃ�����")]
    public float BoostStateUnChangeTime;
    [ Header("�ʏ�̃X�e�[�^�X")]
    public PlayerMoveStatus normalState;
    [Header("�u�[�X�g�̃X�e�[�^�X")]
    public PlayerMoveStatus BoostState;
    [Header("�����̃X�e�[�^�X")]
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


    //�v���C���[�̈ړ�
    private void PlayerMove()
    {

        //�����̈ړ����@
        //Vector3 lef = targetPos.position - transform.position;

        //transform.rotation =
        //    Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lef, Vector3.up), nowState.Bendability);
        //rb.velocity = transform.rotation * Vector3.forward * playerMoveSpeed;

    }


    //player�̃^�[�Q�b�g��ς���Ƃ��ɌĂ�
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


