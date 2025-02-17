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
        param.isInputDown = isInputDown;
        param.isInputUp = isInputUp;
        param.nowEnergyGauge = param.maxEnergyGauge;

        UIMediator.Instance.Init(param);
    }

    // Update is called once per frame
    void Update()
    {
        param.nowEnergyGauge -= 0.01f;
        if (Input.GetKeyDown(KeyCode.Space))
        {


            param.isInputDown = true;
            UIMediator.Instance.Reload(param);
            param.isInputDown = false;
            //Ç±Ç±Ç…âüÇ≥ÇÍÇΩêMçÜÇ™ó~ÇµÇ¢
        }
        if (Input.GetKey(KeyCode.Space))
        {

            param.nowEnergyGauge -= 0.05f;
            UIMediator.Instance.Reload(param);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            param.isInputUp = true;
            UIMediator.Instance.Reload(param);
            param.isInputUp = false;
        }

    }
}
