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
        // �C���v�b�g�V�X�e�����瑗����l�����H���ĕԂ��������L�q���܂�
        float inputValue = Input.GetAxis("Horizontal"); // ��Ƃ���Horizontal���̒l���擾

        // �l�����H����ꍇ�͂����ōs���܂�
        float modifiedValue = inputValue * 100f; // ��Ƃ��Ēl��2�{�ɂ��܂�
        look.m_XAxis.m_InputAxisValue = axis;

        return modifiedValue;
    }

}