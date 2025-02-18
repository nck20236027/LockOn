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
    void Start()
    {
        param = new EnergyGageParam();
        //param.maxEnergyGauge = 200f;
        param.maxEnergyGauge = maxEnergyGauge;
        param.nowEnergyGauge = nowEnergyGauge;
        param.nowEnergyGauge = param.maxEnergyGauge;
        param.buttonState = ButtonState.Non;

        UIMediator.Instance.Init(param);
    }

    // Update is called once per frame
    void Update()
    {
        param.nowEnergyGauge -= 0.01f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            param.buttonState = ButtonState._isInputDown;

            //Ç±Ç±Ç…âüÇ≥ÇÍÇΩêMçÜÇ™ó~ÇµÇ¢
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            param.buttonState = ButtonState._isInputNow;
            param.nowEnergyGauge -= 0.05f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            param.buttonState = ButtonState._isInputUp;
        }

        UIMediator.Instance.Reload(param);
        param.buttonState = ButtonState.Non;
    }
}
