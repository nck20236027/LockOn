public interface IMenuAction
{
    public MenuActionType MenuActionType { get; }
    public void OnMenuAction();
}

public enum MenuActionType
{
    BackGame = 0,
    OpenOption,
    Title,
}