public class PlayerActionManager : ServiceMonoBehaviour<PlayerActionManager>
{
    public PlayerAction playerAction;

    protected override void Awake()
    {
        base.Awake();
        playerAction = new PlayerAction();
        playerAction.Enable();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        playerAction.Disable();
    }
}
