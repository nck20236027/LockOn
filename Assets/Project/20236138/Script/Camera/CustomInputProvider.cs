using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// CinemachineFreeLook 用のカスタム入力プロバイダ。
/// - Input System の入力を受け取り Cinemachine の軸値へ変換して供給する。
/// - 現状は簡易実装で、将来的に横/縦軸のマッピングを拡張する想定。
/// </summary>
public class CustomInputProvider : MonoBehaviour, AxisState.IInputAxisProvider
{
    [SerializeField] 
    private CinemachineFreeLook _look;

    private PlayerAction _action;
    
    private bool _isInputBound;
    
    private Vector2 _axis = Vector2.zero;
    
    private void Start()
    {
        _action = new PlayerAction();
        _action.Enable();
        BindInputActions();
    }

    /// <summary>
    /// カメラ入力用のイベントを登録する。
    /// </summary>
    private void BindInputActions()
    {
        if (_action == null || _isInputBound) return;

        _action.Player.Look.performed += OnLookPerformed;
        _isInputBound = true;
    }

    /// <summary>
    /// 登録済みのカメラ入力イベントを解除する。
    /// </summary>
    private void UnbindInputActions()
    {
        if (_action == null || !_isInputBound) return;

        _action.Player.Look.performed -= OnLookPerformed;
        _isInputBound = false;
    }

    /// <summary>
    /// Look入力をCinemachineの軸入力へ反映する。
    /// </summary>
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        _axis = context.ReadValue<Vector2>();
    }
    
    /// <summary>
    /// Cinemachineから要求された軸値を返す。
    /// </summary>
    public float GetAxisValue(int axis)
    {
        // 簡易に拡大して返す
        float modifiedValue = _axis.x * 100f; 
        _look.m_XAxis.m_InputAxisValue = axis;

        return modifiedValue;
    }

    private void OnDestroy()
    {
        UnbindInputActions();
        _action?.Disable();
        _action?.Dispose();
        _action = null;
    }
}
