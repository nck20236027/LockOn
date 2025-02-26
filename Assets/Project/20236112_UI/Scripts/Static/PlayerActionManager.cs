public class PlayerActionManager : ServiceMonoBehaviour<PlayerActionManager>
{
    public PlayerAction playerAction;

    protected override void Awake()
    {

        base.Awake();
        ServiceLocator<PlayerActionManager>.GetInstance().playerAction = new PlayerAction();
        ServiceLocator<PlayerActionManager>.GetInstance().playerAction.Enable();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        playerAction?.Disable();
        ServiceLocator<PlayerActionManager>.RemoveInstance(this);
    }
}
