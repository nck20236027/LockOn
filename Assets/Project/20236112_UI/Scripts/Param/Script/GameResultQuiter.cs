using System;

public class GameResultQuiter : IResultAction
{
    private GameResultParam _gameResultParam;
    private Action _onSetGameResult;

    public GameResultQuiter(GameResultParam gameResultParam,Action _onSetGameResult)
    {
        this._gameResultParam = gameResultParam;
        this._onSetGameResult = _onSetGameResult;
    }

    public void OnResultAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("GameClear",1f,1f);
        _onSetGameResult?.Invoke();
    }
}
