using System;
using System.Diagnostics;
using UnityEngine;

public class QuitClose : IQuitAction
{
    PauseParam pauseParam;
    QuitParam quitParam;

    public Action onSetQuitCloseInput;


    public QuitActionType QuitActionType => QuitActionType.BackMenu;

    //public Action onSetTitleOpenInput;


    public QuitClose(PauseParam pauseParam, QuitParam quitParam, Action onSetQuitCloseInput)
    {
        this.pauseParam = pauseParam;
        this.quitParam = quitParam;
        this.onSetQuitCloseInput = onSetQuitCloseInput;
    }

    public void OnQuitAction()
    {
        ServiceLocator<UIMediator>.GetInstance().Hide(quitParam);
        ServiceLocator<UIMediator>.GetInstance().Show(pauseParam);
        onSetQuitCloseInput?.Invoke();
        UnityEngine.Debug.Log(0);
    }
}
