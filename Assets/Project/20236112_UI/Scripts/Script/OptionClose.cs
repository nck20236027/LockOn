using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionClose
{
    SettingParam settingParam;
    PauseParam pauseParam;

    public Action onSetOptionCloseInput;

    public OptionClose(PauseParam pauseParam,SettingParam settingParam , Action onSetOptionCloseInput)
    {
        this.pauseParam = pauseParam;
        this.settingParam = settingParam;
        this.onSetOptionCloseInput = onSetOptionCloseInput;
    }


    public void CloseOptionAction()
    {
        UIMediator.Instance.Hide(settingParam);
        UIMediator.Instance.Show(pauseParam);
        onSetOptionCloseInput?.Invoke();
    }
}
