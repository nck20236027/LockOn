using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGageModel : MonoBehaviour
{
    public bool isInputDown = false;
    public bool isInputUp = false;
    public float maxEnergyGauge;
    public float nowEnergyGauge;
    private EnergyGageParam energyGageParam;
    private float energyTimeLost = 1f;

    public float damagePoint;
    void Start()
    {
        energyGageParam = new EnergyGageParam();
        nowEnergyGauge = maxEnergyGauge;
        //param.maxEnergyGauge = 200f;
        energyGageParam.maxEnergyGauge = maxEnergyGauge;
        energyGageParam.nowEnergyGauge = nowEnergyGauge;
        energyGageParam.nowEnergyGauge = energyGageParam.maxEnergyGauge;
        energyGageParam.damageEnergyPoint = damagePoint;
        energyGageParam.energyTimeLost = energyTimeLost;
        energyGageParam.buttonState = ButtonState.Non;

        ServiceLocator<UIMediator>.GetInstance().Init(energyGageParam);
        //Debug.Log($"{1f/200f}");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            energyGageParam.damageEnergyPoint = damagePoint;
            ServiceLocator<UIMediator>.GetInstance().Animation(energyGageParam);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            energyGageParam.buttonState = ButtonState._isInputDown;

            //Ç±Ç±Ç…âüÇ≥ÇÍÇΩêMçÜÇ™ó~ÇµÇ¢
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            energyGageParam.buttonState = ButtonState._isInputNow;
            energyGageParam.energyTimeLost = 20f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            energyGageParam.buttonState = ButtonState._isInputUp;
            energyGageParam.energyTimeLost = 1f;
        }

        ServiceLocator<UIMediator>.GetInstance().Reload(energyGageParam);
        energyGageParam.buttonState = ButtonState.Non;
    }
}
