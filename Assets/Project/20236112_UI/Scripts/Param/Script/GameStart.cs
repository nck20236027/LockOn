using System;

public class GameStart : ITitleAction
{
    private TitleParam _titleParam;
    private Action _onSetGameStart;

    public TitleActionType TitleActionType => TitleActionType.GameStart;


    public GameStart(TitleParam titleParam, Action onSetGameStart)
    {
        this._titleParam = titleParam;
        this._onSetGameStart = onSetGameStart;
    }

    public void OnTitleAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("GameScene", 1f, 1f);
        _onSetGameStart?.Invoke();
    }
}
