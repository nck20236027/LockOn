using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Anchor : MonoBehaviour,ILockTargetable
{
    public Transform GetTransform => transform;

    public Vector3 GetTokenPosition => transform.position;

    public bool GetIsView => _renderer.isVisible;

    private Renderer _renderer;

    [SerializeField]
    private float _recoveryValue = 10;
    float ILockTargetable.ChangeConsuptio(float _consuptio) { return _recoveryValue ;  }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        TargetManager.Instance.AddLockTarget(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        TargetManager.Instance.RemoveLockTarget(this);
    }
}
