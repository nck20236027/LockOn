using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetToken : MonoBehaviour,ILockTargetable
{
    private Renderer renderer;
    public bool GetIsView { get { return renderer.isVisible; } }

    public Vector3 GetTokenPosition => transform.position;

    Transform ILockTargetable.GetTransform => transform;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        TargetManager.Instance.AddLockTarget(this);
    }

    // Update is called once per frame

}
