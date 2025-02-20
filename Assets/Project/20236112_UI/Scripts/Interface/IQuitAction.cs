public interface IQuitAction
{
    public QuitActionType QuitActionType { get; }
    public void OnQuitAction();
}

public enum QuitActionType
{
    BackMenu,
    Quit,
}