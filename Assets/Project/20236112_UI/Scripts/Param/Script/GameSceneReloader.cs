using UnityEngine;

public class GameSceneReloader : IGameOverAction
{
    public GameOverActionType GameOverActionType => GameOverActionType.Restart;
    public void OnGameOverAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("GameScene", 1f, 1f);
        Time.timeScale = 1.0f;
    }
}