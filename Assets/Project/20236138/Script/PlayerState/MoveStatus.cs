using UnityEngine;

/// <summary>
/// プレイヤーの移動に関するパラメータをまとめたクラス群。
/// - MoveStatus: 移動挙動（カーブ、最高速度、旋回性能、減衰など）を保持する基本クラス。
/// - PlayerMoveStatus: プレイヤー固有の移動ステータス（状態種別、燃料消費量など）。
/// </summary>
[System.Serializable]
public class MoveStatus
{
    [SerializeField, Header("移動カーブ")]
    protected AnimationCurve _curve;

    [SerializeField, Header("最大速度")]
    protected float _maxSpeed;

    [SerializeField, Header("旋回しやすさ（小さいほど曲がりにくい）")]
    protected float _bendability;

    [SerializeField, Header("減衰")]
    protected float _damping;

    public AnimationCurve Curve => _curve;
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }
    public float Bendability => _bendability;
    public float Damping => _damping;
    public float Propulsion => MaxSpeed * _damping;
}