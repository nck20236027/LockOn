using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour,IDamagable,ILockTargetable
{
    public Transform GetTransform => transform;

    public Vector3 GetTokenPosition => transform.position;

    public abstract bool GetIsView { get; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        TargetManager.Instance.AddLockTarget(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnDestroy()
    {
        TargetManager.Instance?.RemoveLockTarget(this);
    }

    public abstract void Damage(int damage);
}
