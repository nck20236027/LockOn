using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGageView : ViewBase
{
    [Header("緑、赤（ブースト中）、赤（ダメージ）のゲージ")]
    [SerializeField] private Image _greenGauge;
    [SerializeField] private Image _redBoostGauge;
    [SerializeField] private Image _damageImage;    //アニメーションの場所で行う


    protected override ParamBase GetUseParamBase() => new EnergyGageParam();
    public override void OnInit<T>(T param)
    {
        base.OnInit(param);
        EnergyGageParam energyGageParam = param as EnergyGageParam;
        //_greenGauge.fillAmount = _nowE /energyGageParam.maxEnergyGauge;
        //_greenGauge.fillAmount += _nowE/_maxE ;

        //_nowE /= 200f;
    }
    public async override void OnAnimation<T>(T param)
    {
        EnergyGageParam energyGageParam = param as EnergyGageParam;
        base.OnAnimation<T>(param);
        //_nowE = energyGageParam.damageEnergyPoint / _maxE;
        _damageImage.fillAmount = _greenGauge.fillAmount;//ここで緑と同じ位置にその次表示
        _damageImage.gameObject.SetActive(true);
        _greenGauge.fillAmount -= energyGageParam.damageEnergyPoint/energyGageParam.maxEnergyGauge;//_nowE
        await UniTask.WaitForSeconds(1f);

        _= LMotion.Create(_damageImage.fillAmount, _greenGauge.fillAmount - 0.01f, 1f)//ここのあたいは演出でかえる
        //UniTask.WaitForSeconds(1)

        .WithEase(Ease.OutExpo)
         .WithOnComplete(() => _damageImage.gameObject.SetActive(false)) // Withはどの順番でも大丈夫
        .BindToFillAmount(_damageImage);


        

    }
    public async override void OnReload<T>(T param)
    {
        EnergyGageParam energyGageParam = param as EnergyGageParam;
        base.OnReload(param);
        //_nowE = energyGageParam.nowEnergyGauge;
        _greenGauge.fillAmount -= energyGageParam.energyTimeLost/ energyGageParam.maxEnergyGauge ;
        //_greenGauge.fillAmount = _nowE / energyGageParam.maxEnergyGauge;
        // -= にしないとやばい
        switch (energyGageParam.buttonState)
        {
            case ButtonState._isInputDown:
                _redBoostGauge.fillAmount = _greenGauge.fillAmount;
                _redBoostGauge.gameObject.SetActive(true);
                break;
            case ButtonState._isInputNow:
                //ブースト分引く処理
                break;
            case ButtonState._isInputUp:
                await UniTask.WaitForSeconds(0.5f);
                _=LMotion.Create(_redBoostGauge.fillAmount, _greenGauge.fillAmount - 0.01f, 1f)//ここのあたいは演出でかえる
             .WithEase(Ease.OutExpo)
              .WithOnComplete(() =>
              {
                  _redBoostGauge.gameObject.SetActive(false);
                  //_isInputUp = false;
              }) // Withはどの順番でも大丈夫
             .BindToFillAmount(_redBoostGauge);
                break;
            default:

                break;
        }
        //if (_isInputDown)
        //{
        //    _redBoostGauge.fillAmount = _greenGauge.fillAmount;
        //    _redBoostGauge.gameObject.SetActive(true);
        //    _isInputDown=false;
        //}
        //else if ( _isInputUp==true)
        //{//ブーストが終わった時にリットモーションが動くそのあとに非表示

        //    LMotion.Create(_redBoostGauge.fillAmount, _greenGauge.fillAmount - 0.01f, 1f)//ここのあたいは演出でかえる
        //     .WithEase(Ease.OutExpo)
        //      .WithOnComplete(() =>
        //      {
        //          _redBoostGauge.gameObject.SetActive(false);
        //          _isInputUp=false;
        //      }) // Withはどの順番でも大丈夫
        //     .BindToFillAmount(_redBoostGauge);

        //}
    }
    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new EnergyGageParam()); //すべてのviewにこれを書かないとバグる
    }

}
