using System;
using Unity.VisualScripting;

public class SettingParam : ParamBase
{
    public float nowVolume;
    //public Action closeSettingButton;
    public int currentIndex;
    public float changeAmount;

    public Action<float> OnSetCameraSensitivity;
    public float minCameraSensitiveAffinity;
    public float maxCameraSensitiveAffinity;

}
