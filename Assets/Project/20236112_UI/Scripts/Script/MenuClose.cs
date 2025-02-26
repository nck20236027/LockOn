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
        ServiceLocator<UIMediator>.GetInstance().Hide(pauseParam);
        onSetMenuCloseInput?.Invoke();
    }
}
