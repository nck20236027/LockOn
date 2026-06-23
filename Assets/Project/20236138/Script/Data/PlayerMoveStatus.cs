using UnityEngine;

/// <summary>
/// ロケットのどのステートのデータなのかとロケットのエネルギー消費量
/// </summary>
[System.Serializable]
public class PlayerMoveStatus :MoveStatus
{
    [SerializeField, Header("状態種別（通常/ブースト/減速）")]
    private PlayerStateType _type;
    
    [SerializeField, Header("燃料消費量（単位時間あたり）")]
    private int _fuelConsumption;
    
    public PlayerStateType Type => _type;
    public int FuelConsumption => _fuelConsumption;
    
}