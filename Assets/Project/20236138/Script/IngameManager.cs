using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    private MiniMapParam _miniMapParam = new();
    private CancellationToken _token;
    [SerializeField]
    private CoreEnemy _enemy;
    [SerializeField, Header("ミッションの表示の時間")]
    private float _missionDisplayTime = 3;

    private BannerParam _bannerParam;

    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _CameraTranseform;
    private void Awake()
    {
        _bannerParam = new BannerParam();
        _bannerParam.mainText = "コアを破壊せよ";
        _bannerParam.titleText = "Mission";
    }
    // Start is called before the first frame update
    void Start()
    {
        _miniMapParam.cameraRotationY = _playerTransform.transform.eulerAngles.y;
        _miniMapParam.rocketTransformZ = _playerTransform.transform.position.z;
        _miniMapParam.rocketTransformX = _playerTransform.transform.position.x;
        _token = this.GetCancellationTokenOnDestroy();
        try
        {

        InGameFlow();
        }
        catch {
            Debug.Log("インゲーム終了");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ServiceLocator<UIMediator>.GetInstance().Reload(_miniMapParam);
    }

    private async void InGameFlow()
    {
        ServiceLocator<UIMediator>.GetInstance().Init(_bannerParam);
        ServiceLocator<UIMediator>.GetInstance().Show(_bannerParam);
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);
        await UniTask.Delay(TimeSpan.FromSeconds(_missionDisplayTime), ignoreTimeScale: true,cancellationToken:_token);
        Time.timeScale = 1;
        await UniTask.WaitUntil(() =>  _enemy.nowCoreHp <= 0, cancellationToken: _token);
        //シーン移行
    }
}
