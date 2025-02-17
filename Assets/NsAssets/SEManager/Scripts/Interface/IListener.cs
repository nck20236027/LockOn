using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListener : IServiceClass
{
    public Vector3 ListenerPos { get; }
}
