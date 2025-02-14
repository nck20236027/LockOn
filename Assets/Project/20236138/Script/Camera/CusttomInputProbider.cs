using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CusttomInputProbider : MonoBehaviour, AxisState.IInputAxisProvider
{
    PlayerAction action;
    [SerializeField] CinemachineFreeLook look;
    private void Start()
    {

        action = new PlayerAction();
        action.Enable();
        action.Player.Look.performed += (x) => {
            Vector2 vec = x.ReadValue<Vector2>();
            GetAxisValue((int)vec.sqrMagnitude);

            return;
        };
    }
    public float GetAxisValue(int axis)
    {
        // インプットシステムから送られる値を加工して返す処理を記述します
        float inputValue = Input.GetAxis("Horizontal"); // 例としてHorizontal軸の値を取得

        // 値を加工する場合はここで行います
        float modifiedValue = inputValue * 100f; // 例として値を2倍にします
        look.m_XAxis.m_InputAxisValue = axis;

        return modifiedValue;
    }

}