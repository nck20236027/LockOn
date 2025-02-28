using UnityEngine;

public class GameQuiter : IQuitAction, IGameOverAction
{
    public QuitActionType QuitActionType => QuitActionType.Quit;

    public GameOverActionType GameOverActionType => GameOverActionType.Quit;


    public void OnQuitAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("TitleScene", 1f, 1f);
        Time.timeScale = 1.0f;
    }
    public void OnGameOverAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("TitleScene", 1f, 1f);
        Time.timeScale = 1.0f;
    }
}
