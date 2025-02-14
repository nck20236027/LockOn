using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTitle : MonoBehaviour
{
    PauseParam pauseParam;
    QuitParam quitParam;

    public Action<bool> onTitleInputEnable;
    public Action<bool> onMenuInputEnable;
    public Action onSetTitleOpenInput;

    public MenuActionType MenuActionType => MenuActionType.Title;

    public OpenTitle(PauseParam pauseParam, QuitParam quitParam, Action onSetTitleOpenInput)
    {
        this.pauseParam = pauseParam;
        this.quitParam = quitParam;
        this.onSetTitleOpenInput = onSetTitleOpenInput;
    }


}
