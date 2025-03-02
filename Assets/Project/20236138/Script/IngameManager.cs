using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    CancellationToken token;
    [SerializeField]
    CoreEnemy _enemy;
    [SerializeField, Header("ミッションの表示の時間")]
    float _missionDisplayTime = 3;

    private BannerParam _bannerParam;
    private void Awake()
    {
        _bannerParam = new BannerParam();
        _bannerParam.mainText = "コアを破壊せよ";
        _bannerParam.titleText = "Mission";
    }
    // Start is called before the first frame update
    void Start()
    {
        token = this.GetCancellationTokenOnDestroy();
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
        
    }

    private async void InGameFlow()
    {
        Time.timeScale = 0;
        ServiceLocator<UIMediator>.GetInstance().Init(_bannerParam);
        ServiceLocator<UIMediator>.GetInstance().Show(_bannerParam);
        await UniTask.Delay(TimeSpan.FromSeconds(_missionDisplayTime), ignoreTimeScale: true,cancellationToken:token);
        Time.timeScale = 1;
        await UniTask.WaitUntil(() =>  _enemy.nowCoreHp <= 0, cancellationToken: token);
        //シーン移行
    }
}
