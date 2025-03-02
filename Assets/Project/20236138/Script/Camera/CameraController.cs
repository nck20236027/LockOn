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

public class CameraController : ServiceMonoBehaviour<CameraController>, ICameraContollorable,IServiceClass
{
    [SerializeField] 
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    private CinemachineImpulseSource _impulseSource;
    [SerializeField]
    private CinemachineVirtualCamera _stopCamera;
    [SerializeField]
    private float _maxIntensity;
    public float MaxIntensity { get => _maxIntensity; set => _maxIntensity = value; }
    [SerializeField]
    private float _minIntensity;
    public float MinIntensity { get => _minIntensity; set => _minIntensity = value; }


    [SerializeField, Header("スティックでのカメラ移動のはやさ")]
    float _basisStickSpeed;
    [SerializeField]
    private float _basisintensity = 1;
    public float BasisIntensity { get => _basisintensity; set => _basisintensity = value; }
    [SerializeField, Header("マウスでのカメラ移動のはやさ")]
    float _basisMouseSpeed;
    [SerializeField]
    CinemachineFreeLook _freelook;
    [SerializeField]
    private int _controlInt = 90;

    Vector2 _moveVector = Vector2.zero;
    protected override void Awake()
    {
        base.Awake();
        _basisintensity = GameData._cameraSensitivity;
    }
    void Start()
    {

        PlayerAction actions = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        
        InputAction _stickAction = actions.FindAction("Move");
        _stickAction.Enable();
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


    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameData._cameraSensitivity = _basisintensity;
    }

    public void CameraStop()
    {
        _stopCamera.enabled = true;
        _virtualCamera.enabled = false;
        _freelook.enabled = false;
        _stopCamera.transform.position = _virtualCamera.enabled ? _virtualCamera.transform.position: _freelook.transform.position;
    }
}
