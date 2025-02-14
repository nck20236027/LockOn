using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClose : IMenuAction
{
    PauseParam pauseParam;
    public Action<bool> onCloseMenEnable;
    public Action onSetMenuCloseInput;



    public MenuClose(PauseParam pauseParam,Action onCloseMenuAction)
    {
        this.pauseParam = pauseParam;
        //this.onCloseMenEnable = onCloseMenuAction;
    }

    public MenuActionType MenuActionType => MenuActionType.BackGame;

    public void OnMenuAction()
    {
        UIMediator.Instance.Hide(pauseParam);
    }
}
