using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionClose
{
    SettingParam settingParam;
    PauseParam pauseParam;

    public Action<bool> onOptionInputDisable;
    public Action<bool> onMenuInputEnable;

    public OptionClose(PauseParam pauseParam,SettingParam settingParam ,Action<bool> onOptionInputDisable, Action<bool> onMenuInputEnable)
    {
        this.pauseParam = pauseParam;
        this.settingParam = settingParam;
        this.onOptionInputDisable = onOptionInputDisable;
        this.onMenuInputEnable = onMenuInputEnable;
    }


    public void CloseOptionAction()
    {
        UIMediator.Instance.Hide(settingParam);
        UIMediator.Instance.Show(pauseParam);
        onOptionInputDisable?.Invoke(false);
        onMenuInputEnable?.Invoke(true);
    }
}
