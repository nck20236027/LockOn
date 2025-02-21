using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetToken : MonoBehaviour,ILockTargetable
{
    private Renderer _renderer;
    public bool GetIsView { get { return _renderer.isVisible; } }

    public Vector3 GetTokenPosition => transform.position;

    Transform ILockTargetable.GetTransform => transform;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        TargetManager.Instance.AddLockTarget(this);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        TargetManager.Instance.RemoveLockTarget(this);
    }
}
