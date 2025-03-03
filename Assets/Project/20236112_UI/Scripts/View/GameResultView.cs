public class GameResultView : ViewBase
{
    protected override ParamBase GetUseParamBase() => new GameResultParam();

    public override void OnInit<T>(T param)
    {
        canvas.gameObject.SetActive(false);
    }

    public override void OnShow<T>(T param)
    {
        canvas.gameObject.SetActive(true);
    }

    public override void OnFinal<T>(T param)
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new GameResultParam());
    }
}
