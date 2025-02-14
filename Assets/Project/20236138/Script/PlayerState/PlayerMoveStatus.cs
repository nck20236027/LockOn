using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveStatus
{
    [SerializeField, Header("�X�s�[�h�̑J�ڂ̎d��")]
    protected AnimationCurve _curve;
    public AnimationCurve Curve => _curve;
    [SerializeField, Header("�I�u�W�F�N�g�̍ő�̑���")]
    protected float _maxSpeed;
    public float MaxSpeed => _maxSpeed;
    [SerializeField, Header("�����]���̂��₷��")]
    protected float _bendability;
    public float Bendability => _bendability;

    [SerializeField, Header("��C��R")]
    protected float _damping;
    public float Damping => _damping;
    public float Propulsion => MaxSpeed * _damping;

}


[System.Serializable]
public class PlayerMoveStatus :MoveStatus
{
    [SerializeField, Header("�ړ��̃^�C�v")]
    private PlayerStateType _type;
    public PlayerStateType Type => _type;
    [SerializeField, Header("�R���̏���̑���")]
    private int _fuelConsumptio;
    public int FuelConsumptio => _fuelConsumptio;
    
}


public enum PlayerStateType
{
    Deceleration,
    Normal,
    Boost
}