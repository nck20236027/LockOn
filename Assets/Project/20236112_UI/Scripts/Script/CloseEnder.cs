using System;

public class CloseEnder : IQuitAction
{
    private TitleParam _titleParam;
    private GameEndParam _gameEndParam;

    public Action onSetTitle;

   public CloseEnder(TitleParam _titleParam, GameEndParam _gameEndParam, Action onSetTitle)
    {
        this._titleParam = _titleParam;
        this._gameEndParam = _gameEndParam;
        this.onSetTitle = onSetTitle;
    }

    public QuitActionType QuitActionType => QuitActionType.Quit;

    public void OnQuitAction()
    {
        ServiceLocator<UIMediator>.GetInstance().Hide(_gameEndParam);
        onSetTitle?.Invoke();
        ServiceLocator<UIMediator>.GetInstance().Show(_titleParam);
    }
}
