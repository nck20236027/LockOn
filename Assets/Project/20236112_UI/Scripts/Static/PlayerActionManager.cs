public class PlayerActionManager : ServiceMonoBehaviour<PlayerActionManager>
{
    public PlayerAction playerAction;

    protected override void Awake()
    {

        base.Awake();
        playerAction = new();
        //ServiceLocator<PlayerActionManager>.GetInstance().playerAction = new PlayerAction();
        //ServiceLocator<PlayerActionManager>.GetInstance().playerAction.Enable();
    }
    //protected override void OnDestroy()
    //{
    //    base.OnDestroy();
    //    ServiceLocator<PlayerActionManager>.GetInstance().playerAction?.Disable();
    //    ServiceLocator<PlayerActionManager>.RemoveInstance(this);
    //}
}
