using System;
using Unity.VisualScripting;

public class SettingParam : ParamBase
{
    public float nowVolume;
    //public Action closeSettingButton;
    public int currentIndex;
    public float changeAmount;

    public Action<float> onSetCameraSensitivity;
    public float minCameraSensitiveAffinity;
    public float maxCameraSensitiveAffinity;
    public float initCameraSensitiveAffinity;

    public Action<float> onChangeSEVolue;
    public float minSEVolue;
    public float maxSEVolue;

}
