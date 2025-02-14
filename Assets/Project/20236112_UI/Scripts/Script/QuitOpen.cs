using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOpen : IMenuAction
{
    PauseParam pauseParam;
    QuitParam quitParam;

    public Action<bool> onTitleInputEnable;
    public Action<bool> onMenuInputEnable;
    public Action onSetTitleOpenInput;

    public MenuActionType MenuActionType => MenuActionType.Title;

    public QuitOpen(PauseParam pauseParam, QuitParam quitParam, Action onSetTitleOpenInput)
    {
        this.pauseParam = pauseParam;
        this.quitParam = quitParam;
        this.onSetTitleOpenInput = onSetTitleOpenInput;
    }
    public void OnMenuAction()
    {
        UIMediator.Instance.Show(quitParam);
        onSetTitleOpenInput();
        UIMediator.Instance.Hide(pauseParam);
    }

}
