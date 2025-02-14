//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using UnityEngine;

public class RocetMove
{
    public static void MoveTarget(IMoveObjectable _object,MoveStatus _state,float _time)
    {
        Vector3 toTarget = _object.Gettarget != null ?
            _object.Gettarget.GetTokenPosition - _object.GetPos.position : _object.GetPos.up;
        Vector3 vn = _object.GetRigidbody.velocity.normalized;
        float dot = Vector3.Dot(toTarget, vn);
        Vector3 centripetalAccel = toTarget - (vn * dot);
        float centripetalAccelMagnitude = centripetalAccel.magnitude;
        if (centripetalAccelMagnitude > 1f)
        {
            centripetalAccel /= centripetalAccelMagnitude;
        }
        Vector3 force = centripetalAccel * Mathf.Pow(_state.MaxSpeed, 2) / _state.Bendability;
        force += vn * _state.Propulsion;
        force -= _object.GetRigidbody.velocity * _state.Damping;
        _object.GetRigidbody.velocity += force * Time.fixedDeltaTime * _state.Curve.Evaluate(_time);
        _object.GetPos.rotation =
        Quaternion.Lerp(_object.GetPos.rotation,
            Quaternion.FromToRotation(Vector3.up,
            _state.Curve.Evaluate(_time) == 0f ? toTarget : _object.GetRigidbody.velocity), _state.Bendability);

    }
}
