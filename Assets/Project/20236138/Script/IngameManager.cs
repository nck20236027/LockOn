using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    [SerializeField]
    Player _player;
    [SerializeField]
    CoreEnemy _enemy;
    [SerializeField, Header("ミッションの表示の時間")]
    float _missionDisplayTime = 3;

    private BannerParam _bannerParam;
    private void Awake()
    {
        _bannerParam = new BannerParam();
        _bannerParam.mainText = "1";
        _bannerParam.titleText = "2";
    }
    // Start is called before the first frame update
    void Start()
    {
        //ServiceLocator<UIMediator>.GetInstance().Init(_bannerParam);
        ServiceLocator<UIMediator>.GetInstance().Show(_bannerParam);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InGameFlow()
    {

    }
}
