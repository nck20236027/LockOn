public interface IGameOverAction
{
    public GameOverActionType GameOverActionType { get; }
    public void OnGameOverAction();
}

public enum GameOverActionType
{
    Restart,
    Quit,
}
