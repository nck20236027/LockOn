using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionOpen : IMenuAction
{
    SettingParam settingParam;
    PauseParam pauseParam;

    //public Action<bool> onOptionInputEnable;
    //public Action<bool> onMenuInputEnable;
    public Action onSetOptionOpenInput;

    public MenuActionType MenuActionType => MenuActionType.OpenOption;

    public OptionOpen(SettingParam settingParam,PauseParam pauseParam, Action onSetOptionOpenInput)
    {
        this.settingParam = settingParam;
        this.pauseParam = pauseParam;
        this.onSetOptionOpenInput = onSetOptionOpenInput;
    }

    public void OnMenuAction()
    {
        ServiceLocator<UIMediator>.GetInstance().Show(settingParam);
        onSetOptionOpenInput?.Invoke();
        ServiceLocator<UIMediator>.GetInstance().Hide(pauseParam);
    }

}
