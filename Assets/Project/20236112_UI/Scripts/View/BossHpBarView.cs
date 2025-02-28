using UnityEngine;
using UnityEngine.UI;
using LitMotion;

public class BossHpBarView : ViewBase
{
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Image hpBarBack;
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private float changeTime = 0.1f;
    [SerializeField]
    private Ease changeEase = Ease.Linear;
    protected override ParamBase GetUseParamBase() => new BossHpBarParam();

    public override void OnInit<T>(T param)
    {
        BossHpBarParam bossHpBarParam = param as BossHpBarParam;
        hpBar.fillAmount = bossHpBarParam.bossMaxHp;
        hpText.text = bossHpBarParam.bossName;
    }

    public override void OnAnimation<T>(T param)
    {
        BossHpBarParam bossHpBarParam = param as BossHpBarParam;

        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, bossHpBarParam.bossMaxHp, changeTime);

        

        //Tweens.FillAmountTween(hpBar, hpBar.fillAmount, bossHpBarParam.bossHp, changeTime, changeEase, gameObject);
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
        ServiceLocator<UIMediator>.GetInstance().Final(new BossHpBarParam());
    }
}
