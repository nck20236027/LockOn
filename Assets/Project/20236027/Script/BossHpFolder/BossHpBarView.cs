using UnityEngine;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;

public class BossHpBarView : ViewBase
{
    [SerializeField]
    private Image _hpBar;
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private float changeTime = 0.1f;

    protected override ParamBase GetUseParamBase() => new BossHpBarParam();

    public override void OnInit<T>(T param)
    {
        BossHpBarParam bossHpBarParam = param as BossHpBarParam;

        //_hpBar.fillAmount = hpBarBack.fillAmount;
        // _hpBar.fillAmount = bossHpBarParam.bossNowHp / bossHpBarParam.bossMaxHp;
        Debug.Log(bossHpBarParam.bossNowHp / bossHpBarParam.bossMaxHp);
        hpText.text = bossHpBarParam.bossName;
    }

    public override void OnAnimation<T>(T param)
    {
        BossHpBarParam bossHpBarParam = param as BossHpBarParam;
        float HoldHp = bossHpBarParam.bossNowHp / bossHpBarParam.bossMaxHp;
        LMotion.Create(_hpBar.fillAmount, HoldHp - 0.01f, 1f)//Ç±Ç±ÇÃÇ†ÇΩÇ¢ÇÕââèoÇ≈Ç©Ç¶ÇÈ
        //UniTask.WaitForSeconds(1)

        .WithEase(Ease.OutExpo)
        // .WithOnComplete(() => _damageImage.gameObject.SetActive(false)) // WithÇÕÇ«ÇÃèáî‘Ç≈Ç‡ëÂè‰ïv
        .BindToFillAmount(_hpBar);

        //_hpBar.fillAmount = Mathf.Lerp(_hpBar.fillAmount, bossHpBarParam.bossMaxHp, changeTime);



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

    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new BossHpBarParam());
    }
}
