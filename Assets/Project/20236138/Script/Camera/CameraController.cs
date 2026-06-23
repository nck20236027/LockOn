using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// カメラ周りの操作を行うコントローラクラス。
/// - Cinemachine を使った仮想カメラの切り替えや揺れ（インパルス）生成を行う。
/// - 入力（スティック／マウス）に応じてフリールックを回転させる。
/// </summary>
public class CameraController : ServiceMonoBehaviour<CameraController>, ICameraControllable,IServiceClass
{
    // Inspector で設定する値
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    [SerializeField]
    private CinemachineImpulseSource _impulseSource;

    [SerializeField]
    private CinemachineVirtualCamera _stopCamera;

    [SerializeField]
    private float _maxIntensity;

    [SerializeField]
    private float _minIntensity;

    [SerializeField, Header("入力感度（スティック）")]
    private float _basisStickSpeed;

    [SerializeField]
    private float _basisIntensity = 1;

    [SerializeField, Header("入力感度（マウス）")]
    private float _basisMouseSpeed;

    [SerializeField]
    private CinemachineFreeLook _freelook;

    [SerializeField]
    private int _controlInt = 90;

    [SerializeField]
    private float _verticalSensitivity = 1.0f;

    // 内部状態
    private Vector2 _moveVector = Vector2.zero;
    
    private InputAction _lookAction;
    
    private bool _isInputBound;

    // 公開プロパティ
    public float MaxIntensity { get => _maxIntensity; set => _maxIntensity = value; }
    
    public float MinIntensity { get => _minIntensity; set => _minIntensity = value; }
    
    public float BasisIntensity { get => _basisIntensity; set => _basisIntensity = value; }

    protected override void Awake()
    {
        base.Awake();
        _basisIntensity = GameData.cameraSensitivity;
    }

    /// <summary>
    /// カメラ入力を共有InputActionへ登録する。
    /// </summary>
    private void Start()
    {
        PlayerAction actions = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        BindCameraInput(actions.FindAction("Look"));
    }

    /// <summary>
    /// カメラ移動用の入力イベントを登録する。
    /// </summary>
    private void BindCameraInput(InputAction lookAction)
    {
        if (lookAction == null || _isInputBound) return;

        _lookAction = lookAction;
        _lookAction.Enable();
        _lookAction.performed += OnCameraMoveChanged;
        _lookAction.canceled += OnCameraMoveChanged;
        _isInputBound = true;
    }

    /// <summary>
    /// 登録済みのカメラ入力イベントを解除する。
    /// </summary>
    private void UnbindCameraInput()
    {
        if (_lookAction == null || !_isInputBound) return;

        _lookAction.performed -= OnCameraMoveChanged;
        _lookAction.canceled -= OnCameraMoveChanged;
        _isInputBound = false;
    }

    /// <summary>
    /// カメラ移動入力の値を保持する。
    /// </summary>
    private void OnCameraMoveChanged(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // 入力に従ってフリールックの軸値を更新する
        _freelook.m_XAxis.Value += _moveVector.x * _basisIntensity * _controlInt;
        _freelook.m_YAxis.Value -= _moveVector.y * _basisIntensity * _verticalSensitivity;
    }
    
    /// <summary>
    /// カメラの感度を設定する（外部から呼ぶ）
    /// </summary>
    public void SetStickSpeed(float speed)
    {
        _basisIntensity = speed;   
    }

    /// <summary>
    /// カメラのシェイク（インパルス）を発生させる
    /// </summary>
    public void CameraShake()
    {
        _impulseSource.GenerateImpulse();
    }

    /// <summary>
    /// 仮想カメラの有効/無効を切り替える
    /// </summary>
    public void CameraChange()
    {
        _virtualCamera.enabled = !_virtualCamera.enabled;
    }

    /// カメラ入力イベントを解除してからオブジェクトを破棄する
    protected override void OnDestroy()
    {
        UnbindCameraInput();
        base.OnDestroy();
        GameData.cameraSensitivity = _basisIntensity;
    }

    /// <summary>
    /// カメラ停止モードに切り替える（停止用カメラを有効にする）
    /// </summary>
    public void CameraStop()
    {
        _stopCamera.enabled = true;
        _virtualCamera.enabled = false;
        _freelook.enabled = false;
        _stopCamera.transform.position = _virtualCamera.transform.position;
    }
}
