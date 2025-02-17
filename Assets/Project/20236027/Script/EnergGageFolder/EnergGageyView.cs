using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGageView : ViewBase
{
    [Header("緑、赤（ブースト中）、赤（ダメージ）のゲージ")]
    [SerializeField] private Image _greenGauge;
    [SerializeField] private Image _redBoostGauge;
    [SerializeField] private Image _redDamageGauge;
    private GameObject red;
    private float _maxE;
    private float _nowE;
    private bool _isInputDown = false;
    private bool _isInputUp = false;


    protected override ParamBase GetUseParamBase() => new EnergyGageParam();
    public override void OnAnimation<T>(T param)
    {
        //ここはアニメーションじゃなくてinputボタン系の　paramでやる
        //だから押したとき離した時に呼ぶUIManagerの処理がいる追加する相談


    }
    public override void OnInit<T>(T param)
    {
        base.OnInit(param);
        EnergyGageParam energyGageParam = param as EnergyGageParam;
        _maxE = energyGageParam.maxEnergyGauge;
        _nowE = energyGageParam.nowEnergyGauge;
        //_greenGauge.fillAmount = _nowE / 200f;

        //_nowE /= 200f;

    }
    public override void OnReload<T>(T param)
    {
        EnergyGageParam energyGageParam = param as EnergyGageParam;
        base.OnReload(param);
        _nowE = energyGageParam.nowEnergyGauge;
        _isInputDown = energyGageParam.isInputDown;
        _isInputUp = energyGageParam.isInputUp;
        if (_isInputDown)
        {
            //_redBoostGauge.
            _redBoostGauge.fillAmount = _greenGauge.fillAmount;
            _redBoostGauge.gameObject.SetActive(true);
            //LMotion.Create()
            _isInputDown=false;
        }
        else if ( _isInputUp==true)
        {//ブーストが終わった時にリットモーションが動くそのあとに非表示

            LMotion.Create(_redBoostGauge.fillAmount, _greenGauge.fillAmount - 0.01f, 1f)//ここのあたいは演出でかえる
             .WithEase(Ease.OutExpo)
              .WithOnComplete(() =>
              {
                  _redBoostGauge.gameObject.SetActive(false);
                  _isInputUp=false;
              }) // Withはどの順番でも大丈夫
             .BindToFillAmount(_redBoostGauge);
            //_redBoostGauge.gameObject.SetActive(false);


        }


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //_nowE = energyGageParam.nowEnergyGauge;
        _greenGauge.fillAmount = _nowE / 200f;
    }
}
