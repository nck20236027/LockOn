using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOpen : IMenuAction

{
    PauseParam pauseParam;
    QuitParam quitParam;

    public Action onQuitInputEnable;

    public MenuActionType MenuActionType => MenuActionType.Quit;

    public QuitOpen(PauseParam pauseParam, QuitParam quitParam, Action onQuitInputEnable)
    {
        this.pauseParam = pauseParam;
        this.quitParam = quitParam;
        this.onQuitInputEnable = onQuitInputEnable;
    }
    public void OnMenuAction()
    {
        UIMediator.Instance.Show(quitParam);
        onQuitInputEnable?.Invoke();
        UIMediator.Instance.Hide(pauseParam);
    }

}
