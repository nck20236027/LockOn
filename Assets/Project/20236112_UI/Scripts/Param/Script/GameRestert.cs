using System;

public class GameRestert : IGameOverAction
{
    private GameOverParam _gameOverParam;
    private Action _onRestert;

    public GameRestert(GameOverParam gameOverParam,Action onSetRestert)
    {
        this._gameOverParam = gameOverParam;
        this._onRestert = onSetRestert;
    }

    public GameOverActionType GameOverActionType => throw new System.NotImplementedException();

    public void OnGameOverAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("20236112_UI", 1f, 1f);
        _onRestert?.Invoke();
    }
}
