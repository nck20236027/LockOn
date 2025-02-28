using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//
public class BoostEffectModel : MonoBehaviour
{
    [SerializeField] private Transform _rocketPosition;
    [SerializeField] private Transform _cameraPosition;
    private BoostEffectParam boostEffectParam = new BoostEffectParam();

    void Start()
    {
        boostEffectParam.rocketSpeedState = RocketSpeedState.Non;
        ServiceLocator<UIMediator>.GetInstance().Init(boostEffectParam);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            boostEffectParam.rocketSpeedState = RocketSpeedState._isHighSpeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            boostEffectParam.effectRocketPosition = _rocketPosition.position;
            boostEffectParam.effectRocketRotation = _rocketPosition.rotation;

            boostEffectParam.effectCameraPosition = _cameraPosition.position;
            boostEffectParam.effectCameraRotation = _cameraPosition.rotation;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            boostEffectParam.rocketSpeedState = RocketSpeedState._isNormalSpeed;
        }
        ServiceLocator<UIMediator>.GetInstance().Animation(boostEffectParam);
    }
}
