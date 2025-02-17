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

public class CameraController : MonoBehaviour, ICameraContollorable,IServiceClass
{
#if a
    [SerializeField]
    InputAction action;
    [SerializeField, Header("振動の最大の強さ")]
    private float shaikhPowor;
    [SerializeField, Header("振動の揺れの間隔")]
    private float shaikhInterval;

     public CinemachineInputProvider inputProvider;


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

    [SerializeField] 
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    private CinemachineImpulseSource _impulseSource;

    public float MaxIntensity;
    public float MinIntensity;

    [SerializeField]
    InputAction _stickAction = new InputAction();

    [SerializeField, Header("スティックでのカメラ移動のはやさ")]
    float _basisStickSpeed;
    public float _basisintensity = 1;
    [SerializeField, Header("マウスでのカメラ移動のはやさ")]
    float _basisMouseSpeed;
    [SerializeField]
    CinemachineFreeLook _freelook;
    private int _controlInt = 90;

    Vector2 _moveVector = Vector2.zero;
    void Start()
    {
        ServiceLocator<CameraController>.Register(this);
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
                _moveVector = vec;
            };
        _stickAction.canceled +=
            (x) =>
            {
                Vector2 vec = x.ReadValue<Vector2>();
                _moveVector = vec;
            };
        _stickAction.Enable();
    }

    private void Update()
    {
        _freelook.m_XAxis.Value += _moveVector.x * _basisintensity * _controlInt;
        _freelook.m_YAxis.Value += _moveVector.y * _basisintensity;
    }
    public void SetStickSpeed(float speed)
    {
        _basisintensity = speed;
        
    }

    public void CameraSheikh()
    {
        _impulseSource.GenerateImpulse();
    }

    public void CameraChange()
    {
        _virtualCamera.enabled = !_virtualCamera.enabled;
    }
    
}
