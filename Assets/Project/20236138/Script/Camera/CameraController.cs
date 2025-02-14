using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CameraType
{
    NormalCamera,
    BoostCamera,

}

public class CameraController : MonoBehaviour, ICameraContollorable
{
#if a
    [SerializeField]
    InputAction action;
    [SerializeField, Header("�U���̍ő�̋���")]
    private float shaikhPowor;
    [SerializeField, Header("�U���̗h��̊Ԋu")]
    private float shaikhInterval;

     public CinemachineInputProvider inputProvider;


    [SerializeField] 
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    private CinemachineImpulseSource _impulseSource;
    public void CameraSheikh()
    {
        _impulseSource.GenerateImpulse();
    }

    public void CameraChange()
    {
        _virtualCamera.enabled = !_virtualCamera.enabled;
    }

    private void Start()
    {

        for(int i = 0; action.bindings.Count > i; i++)
        {
            Debug.Log(action.bindings[i].processors);
        }
            
        ;
        _impulseSource.m_ImpulseDefinition.m_AmplitudeGain = shaikhPowor;
        _impulseSource.m_ImpulseDefinition.m_FrequencyGain = shaikhInterval;

    }
#endif
    [SerializeField] private InputAction _inputAction;

    private void Awake()
    {
        // Input Action�̐���
        _inputAction = new InputAction(
            "TestAction",           // Action��
            InputActionType.Button, // Action Type
            "<Keyboard>/A",         // Binding��Control Path
            "hold",                 // Interaction
            "scale(factor=2.5)",    // Processor
            "Button"                // ��ނ̐���
        );

        // performed�R�[���o�b�N���󂯎��悤�ɐݒ�
        _inputAction.performed += OnReadAction;

        // ���͂̎󂯎���L��������K�v������
        _inputAction.Enable();
    }

    private void OnDestroy()
    {
        // �I������Action�𖳌�������
        _inputAction?.Disable();
    }

    // TestAction��performed�R�[���o�b�N
    private void OnReadAction(InputAction.CallbackContext context)
    {
        // �󂯎�����l�����O�o��
        Debug.Log($"���͒l : {context.ReadValue<float>()}");
    }






    [SerializeField]
    InputAction _stickAction = new InputAction();
    [SerializeField]
    InputAction _mouseAction;
    public Transform Target { get => target; set => target = value; }
    private Transform target;
    Vector3 position = Vector3.zero;
    [SerializeField]
    Vector2 cameraRotationPoint = Vector2.zero;

    [SerializeField, Header("�X�e�B�b�N�ł̃J�����ړ��̂͂₳")]
    float _basisStickSpeed;
    float _stickintensity = 1;
    [SerializeField, Header("�}�E�X�ł̃J�����ړ��̂͂₳")]
    float _basisMouseSpeed;
    [SerializeField]
    CinemachineFreeLook _freelook;

    void Start()
    {
        _stickAction = new InputAction(
            "Move",
            InputActionType.PassThrough,
            expectedControlType:"Vector2");

        _stickAction.AddBinding("<Gamepad>/RightStick").
            WithProcessor($"scalevector2(x={_basisStickSpeed},y={_basisStickSpeed})").WithName("Mouse");
        _stickAction.AddBinding("<Mouse>/Delta").
    WithProcessor($"scalevector2(x={_basisMouseSpeed},y={_basisMouseSpeed})").WithName("stick");

        _stickAction.performed +=
            (x) =>
            {
                Vector2 vec = x.ReadValue<Vector2>();
                _freelook.m_XAxis.Value += vec.x * _stickintensity * 90;
                _freelook.m_YAxis.Value += vec.y * _stickintensity ;
            };
        _stickAction.Enable();
    }
    public void SetStickSpeed(float speed)
    {
        _stickintensity = speed;
        
    }

    public void CameraChange()
    {
        throw new System.NotImplementedException();
    }

    public void CameraSheikh()
    {
        throw new System.NotImplementedException();
    }
}
