using System;

public class GameStart : ITitleAction
{
    private TitleParam _titleParam;
    private Action onSetGameStart;

    public TitleActionType TitleActionType => TitleActionType.GameStart;

    public GameStart(TitleParam titleParam,Action onSetGameStart)
    {
        this._titleParam = titleParam;
        this.onSetGameStart = onSetGameStart;
    }

    public void OnTitleAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("20236112_UI", 1f, 1f);
        onSetGameStart?.Invoke();
    }
}
