using UnityEngine;

public class BoostEffectParam : ParamBase 
{
    public Vector3 effectRocketPosition;
    public Quaternion effectRocketRotation;

    public Vector3 effectCameraPosition;
    public Quaternion effectCameraRotation;

    public RocketSpeedState rocketSpeedState;
}
public enum RocketSpeedState
{
    _isNormalSpeed,
    _isHighSpeed,
    Non,
}
