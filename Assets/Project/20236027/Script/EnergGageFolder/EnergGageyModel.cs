using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGageModel : MonoBehaviour
{
    public bool isInputDown = false;
    public bool isInputUp = false;
    public float maxEnergyGauge;
    public float nowEnergyGauge;
    private EnergyGageParam param;
    private float energyTimeLost = 1f;

    public float damagePoint;
    void Start()
    {
        param = new EnergyGageParam();
        nowEnergyGauge = maxEnergyGauge;
        //param.maxEnergyGauge = 200f;
        param.maxEnergyGauge = maxEnergyGauge;
        param.nowEnergyGauge = nowEnergyGauge;
        param.nowEnergyGauge = param.maxEnergyGauge;
        param.damageEnergyPoint = damagePoint;
        param.energyTimeLost = energyTimeLost;
        param.buttonState = ButtonState.Non;

        UIMediator.Instance.Init(param);
        Debug.Log($"{1f/200f}");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            param.damageEnergyPoint = damagePoint;
            UIMediator.Instance.Animation(param);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            param.buttonState = ButtonState._isInputDown;

            //Ç±Ç±Ç…âüÇ≥ÇÍÇΩêMçÜÇ™ó~ÇµÇ¢
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            param.buttonState = ButtonState._isInputNow;
            param.energyTimeLost = 20f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            param.buttonState = ButtonState._isInputUp;
            param.energyTimeLost = 1f;
        }

        UIMediator.Instance.Reload(param);
        param.buttonState = ButtonState.Non;
    }
}
