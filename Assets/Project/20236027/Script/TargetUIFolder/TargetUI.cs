using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour, IhasTargetPos
{

    [SerializeField]
    Transform target;
    public Vector3 GetPos => target.position;

    private TargetUIParam param = new TargetUIParam();
    void Start()
    {
        param.targetPos = this;
        UIMediator.Instance.Init(param);
        UIMediator.Instance.Animation(param);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            UIMediator.Instance.Hide(param);
        }
        if(Input.GetMouseButtonDown(1))
        {
            UIMediator.Instance.Show(param);
        }
    }
}
