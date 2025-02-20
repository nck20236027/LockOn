using System;

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
        UIMediator.Instance.Hide(quitParam);
        UIMediator.Instance.Show(pauseParam);
        onSetQuitCloseInput?.Invoke();
    }
}
