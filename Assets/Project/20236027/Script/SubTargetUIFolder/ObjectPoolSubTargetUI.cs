using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSubTargetUI : MonoBehaviour
{
    private GameObject target;
    public GameObject Target { get => target; set => target = value; }
    //public void Release()
    //{
    //    target.ReturnToPool(this);
    //}
}
