using System;

public class OpenGameEnd : ITitleAction
{
    private TitleParam _titleParam;
    private GameEndParam _gameEndParam;

    public Action onSetGameEnd;

    public TitleActionType TitleActionType => TitleActionType.GameEnd;

    public OpenGameEnd(TitleParam titleParam, Action onSetGameEnd)
    {
        this._titleParam = titleParam;
        this.onSetGameEnd = onSetGameEnd;
    }

    public void OnTitleAction()
    {
        UIMediator.Instance.Show(_gameEndParam);
        onSetGameEnd?.Invoke();
        UIMediator.Instance.Hide(_titleParam);
    }
}
