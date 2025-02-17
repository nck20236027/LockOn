public class GameQuiter : IQuitAction
{
    public QuitActionType QuitActionType => QuitActionType.Title;

    //public GameQuiter()
    //{
    //    OnQuitAction();
    //}

    public void OnQuitAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("TitleScene", 1f, 1f);
    }
}
