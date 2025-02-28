using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffectView : ViewBase
{
    [SerializeField] private GameObject _boostEffectPosition;
    [SerializeField] private ParticleSystem _boostParticleEffect;
    [SerializeField] private GameObject _cameraEffectPosition;

    protected override ParamBase GetUseParamBase() => new BoostEffectParam();

    public override void OnInit<T>(T param)
    {
        base.OnInit(param);
        BoostEffectParam boostEffectParam = param as BoostEffectParam;
        //_boostEffectPosition.transform.position = boostEffectParam.effectRocketPosition;
        //_boostEffectPosition.transform.rotation = boostEffectParam.effectRocketRotation;

        //_cameraEffectPosition.transform.position = boostEffectParam.effectCameraPosition;
        //_cameraEffectPosition.transform.rotation = boostEffectParam.effectCameraRotation;
    }
    public override void OnAnimation<T>(T param)
    {
        base.OnAnimation(param);
        BoostEffectParam boostEffectParam = param as BoostEffectParam;

        switch (boostEffectParam.rocketSpeedState)
        {
            case RocketSpeedState._isNormalSpeed:
                
                _boostEffectPosition.SetActive(false);
                _cameraEffectPosition.SetActive(false);
                break;
            case RocketSpeedState._isHighSpeed:
        _boostEffectPosition.transform.position = boostEffectParam.effectRocketPosition;
        _boostEffectPosition.transform.rotation = boostEffectParam.effectRocketRotation;

        _cameraEffectPosition.transform.position = boostEffectParam.effectCameraPosition;
        _cameraEffectPosition.transform.rotation = boostEffectParam.effectCameraRotation;
                _boostEffectPosition.SetActive(true);
                _cameraEffectPosition.SetActive(true);

                break;
            default:
                break;
        }

    }
    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new BoostEffectParam()); //Ç∑Ç◊ÇƒÇÃviewÇ…Ç±ÇÍÇèëÇ©Ç»Ç¢Ç∆ÉoÉOÇÈ
    }
    private void Update()
    {

    }

}
