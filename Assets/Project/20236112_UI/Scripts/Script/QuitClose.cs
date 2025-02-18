using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitClose : IQuitAction
{
    PauseParam pauseParam;
    QuitParam quitParam;

    public Action<bool> onQuitInputDisable;
    public Action<bool> onMenuInputEnable;

    public QuitActionType QuitActionType => QuitActionType.BackMenu;

    //public Action onSetTitleOpenInput;


    public QuitClose(PauseParam pauseParam, QuitParam quitParam, Action<bool> onQuitInputDisable, Action<bool> onMenuInputEnable)
    {
        this.pauseParam = pauseParam;
        this.quitParam = quitParam;
        this.onQuitInputDisable = onQuitInputDisable;
        this.onMenuInputEnable = onMenuInputEnable;

    }

    //public void OnTitleAction()
    //{
    //    onMenuInputDisable?.Invoke(true);
    //    onTitleInputEnable?.Invoke(false);
    //}

    public void OnQuitAction()
    {
        UIMediator.Instance.Hide(quitParam);
        UIMediator.Instance.Show(pauseParam);
        onQuitInputDisable?.Invoke(false);
        onMenuInputEnable?.Invoke(true);

    }
}
