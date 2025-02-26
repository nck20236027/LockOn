using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour, IhasTargetPos
{

    [SerializeField]
    TargetToken target;
    public ILockTargetable GetTarget => target;

    private TargetUIParam targetParam = new TargetUIParam();
    void Start()
    {
        targetParam.targetPos = this;
        ServiceLocator<UIMediator>.GetInstance().Init(targetParam);
        ServiceLocator<UIMediator>.GetInstance().Animation(targetParam);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            ServiceLocator<UIMediator>.GetInstance().Hide(targetParam);
        }
        if (Input.GetMouseButtonDown(1))
        {
            ServiceLocator<UIMediator>.GetInstance().Show(targetParam);
        }
    }
}
