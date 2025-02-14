using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitClose : MonoBehaviour
{
    PauseParam pauseParam;
    QuitParam quitParam;

    public Action<bool> onTitleInputEnable;
    public Action<bool> onMenuInputDisable;
    //public Action onSetTitleOpenInput;

    public MenuActionType MenuActionType => MenuActionType.Title;

    public QuitClose(PauseParam pauseParam, QuitParam quitParam, Action<bool> onTitleInputEnable, Action<bool> onMenuInputDisable)
    {
        this.pauseParam = pauseParam;
        this.quitParam = quitParam;
        this.onMenuInputDisable = onMenuInputDisable;
        this.onTitleInputEnable = onTitleInputEnable;
    }

    public void OnTitleAction()
    {
        UIMediator.Instance.Show(quitParam);
        onMenuInputDisable?.Invoke(true);
        onTitleInputEnable?.Invoke(false);
        UIMediator.Instance.Hide(pauseParam);
    }
}
