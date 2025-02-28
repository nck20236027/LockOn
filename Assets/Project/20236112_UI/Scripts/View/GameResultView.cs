using LitMotion;
using UnityEngine;
using UnityEngine.UI;


public class GameResultView : ViewBase
{

    

    [SerializeField]
    private Ease changeEase = Ease.Linear;

    [SerializeField]
    private float _expandTime = 0.1f;
    [SerializeField]
    private float _reduceTime = 0.1f;


    protected override ParamBase GetUseParamBase() => new GameResultParam();

    public override void OnInit<T>(T param)
    {
        canvas.gameObject.SetActive(true);
    }

    public override void OnAnimation<T>(T param)
    {

    }

    public override void OnShow<T>(T param)
    {
        canvas.gameObject.SetActive(true);
    }
    public override void OnHide<T>(T param)
    {
        canvas.gameObject.SetActive(false);
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
