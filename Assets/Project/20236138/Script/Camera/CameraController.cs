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


    [SerializeField]
    InputAction _stickAction = new InputAction();
    [SerializeField]
    InputAction _mouseAction;
    public Transform Target { get => target; set => target = value; }
    private Transform target;

    [SerializeField, Header("スティックでのカメラ移動のはやさ")]
    float _basisStickSpeed;
    float _stickintensity = 1;
    [SerializeField, Header("マウスでのカメラ移動のはやさ")]
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

    public void CameraSheikh()
    {
        _impulseSource.GenerateImpulse();
    }

    public void CameraChange()
    {
        _virtualCamera.enabled = !_virtualCamera.enabled;
    }
    
}
