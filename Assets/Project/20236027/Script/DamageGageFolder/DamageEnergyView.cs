using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnergyView : ViewBase
{
    [Header("緑、赤（ダメージを食らった時）のゲージ")]
    [SerializeField] private Image _greenImage;
    [SerializeField] private Image _damageImage;
    private float _damagePoint;
    private float _maxDamage;
    protected override ParamBase GetUseParamBase() => new DamageEnergyParam();
    public override void OnInit<T>(T param)
    {
        DamageEnergyParam energyGageParam = param as DamageEnergyParam;
        base.OnInit(param);
        _maxDamage = energyGageParam.damageEnergyPoint;
        _damagePoint = _maxDamage/energyGageParam.damageEnergyPoint;

    }

    public override void OnAnimation<T>(T param)
    {
        DamageEnergyParam energyGageParam = param as DamageEnergyParam;
        base.OnAnimation(param);
        Debug.Log($"dadwadwadadwad");
        _damageImage.fillAmount = _greenImage.fillAmount;//ここで緑と同じ位置にその次表示
        _damageImage.gameObject.SetActive(true);
        _damagePoint = 0.2f;
            //_maxDamage / energyGageParam.damageEnergyPoint;
        _greenImage.fillAmount -= _damagePoint;//緑がダメージ分食らう
 //非同期処理で　一秒後に赤が動き出すようにする 多和田に教えてもらう

        //await UniTask.Delay(TimeSpan.FromSeconds(1f));

        LMotion.Create(_damageImage.fillAmount, _greenImage.fillAmount - 0.01f, 1f)//ここのあたいは演出でかえる
        .WithEase(Ease.OutExpo)
         .WithOnComplete(() => _damageImage.gameObject.SetActive(false)) // Withはどの順番でも大丈夫
        .BindToFillAmount(_damageImage);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
