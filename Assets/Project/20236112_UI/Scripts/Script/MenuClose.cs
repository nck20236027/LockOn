using System;

public class MenuClose : IMenuAction
{
    PauseParam pauseParam;
    public Action onSetMenuCloseInput;

    public MenuActionType MenuActionType => MenuActionType.BackGame;


    public MenuClose(PauseParam pauseParam,Action onSetMenuCloseInput)
    {
        this.pauseParam = pauseParam;
        this.onSetMenuCloseInput = onSetMenuCloseInput;
    }


    public void OnMenuAction()
    {
        UIMediator.Instance.Hide(pauseParam);
        onSetMenuCloseInput?.Invoke();
    }
}
