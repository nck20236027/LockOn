using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnergyModel : MonoBehaviour
{
    private DamageEnergyParam param;
    public float damagePoint;
    void Start()
    {
        param = new DamageEnergyParam();
        UIMediator.Instance.Init(param);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIMediator.Instance.Animation(param);
        }
    }
}
