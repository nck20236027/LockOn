using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ターゲットに指定するためのインターフェース
public interface ILockTargetable
{
    public Transform GetTransform { get; }
    public Vector3 GetTokenPosition { get; }
    public bool GetIsView { get; }

}
