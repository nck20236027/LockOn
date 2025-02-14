using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveStatus
{
    [SerializeField, Header("スピードの遷移の仕方")]
    protected AnimationCurve _curve;
    public AnimationCurve Curve => _curve;
    [SerializeField, Header("オブジェクトの最大の速さ")]
    protected float _maxSpeed;
    public float MaxSpeed => _maxSpeed;
    [SerializeField, Header("方向転換のしやすさ")]
    protected float _bendability;
    public float Bendability => _bendability;

    [SerializeField, Header("空気抵抗")]
    protected float _damping;
    public float Damping => _damping;
    public float Propulsion => MaxSpeed * _damping;

}


[System.Serializable]
public class PlayerMoveStatus :MoveStatus
{
    [SerializeField, Header("移動のタイプ")]
    private PlayerStateType _type;
    public PlayerStateType Type => _type;
    [SerializeField, Header("燃料の消費の速さ")]
    private int _fuelConsumptio;
    public int FuelConsumptio => _fuelConsumptio;
    
}


public enum PlayerStateType
{
    Deceleration,
    Normal,
    Boost
}