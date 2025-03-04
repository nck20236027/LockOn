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
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("GameScene", 1f, 0f);
        _onRestert?.Invoke();
    }
}
