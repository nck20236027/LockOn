using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveObjectable
{
    public Transform GetPos { get; }

    public Rigidbody GetRigidbody { get; }
    //ロックオンのターゲット
    public
    Vector3 Gettarget { get; }
}
