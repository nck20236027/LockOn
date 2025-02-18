using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOpen : IMenuAction
{
    PauseParam pauseParam;
    QuitParam quitParam;

    public Action<bool> onQuitInputEnable;
    public Action<bool> onMenuInputEnable;

    public MenuActionType MenuActionType => MenuActionType.Quit;

    public QuitOpen(PauseParam pauseParam, QuitParam quitParam, Action<bool> onQuitInputEnable, Action<bool> onMenuInputEnable)
    {
        this.pauseParam = pauseParam;
        this.quitParam = quitParam;
        this.onQuitInputEnable = onQuitInputEnable;
        this.onMenuInputEnable = onMenuInputEnable;
    }
    public void OnMenuAction()
    {
        UIMediator.Instance.Show(quitParam);
        onQuitInputEnable?.Invoke(true);
        onMenuInputEnable?.Invoke(false);
        UIMediator.Instance.Hide(pauseParam);
    }

}
