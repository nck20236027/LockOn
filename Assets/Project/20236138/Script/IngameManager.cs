using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// ゲーム内のマネージャクラス。
/// - ミニマップやバナー表示、ゲームフロー（開始からクリアまで）を制御する。
/// - UniTask を使った非同期処理でミッション表示や勝利判定を行う。
/// </summary>
public class IngameManager : MonoBehaviour
{
    [SerializeField]
    private CoreEnemy _enemy;
   
    [SerializeField, Header("ミッション表示時間（秒）")]
    private float _missionDisplayTime = 3;

    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private Transform _cameraTransform;
   
    [SerializeField]
    private string _bannerMessage;
   
    [SerializeField]
    private string _bannerTitle;

    private MiniMapParam _miniMapParam;
    private CancellationToken _token;
    private BannerParam _bannerParam;

    private void Awake()
    {
        _bannerParam = new BannerParam();
        _bannerParam.mainText = _bannerMessage;
        _bannerParam.titleText = _bannerTitle;
    }
    
    /// <summary>
    /// ゲーム開始時のUI初期化とゲーム進行フローの起動を行う。
    /// </summary>
    void Start()
    {
        // ミニマップ初期化と UI の初期化、ゲームフロー開始
        _miniMapParam.cameraRotationY = _playerTransform.transform.eulerAngles.y;
        _miniMapParam.rocketTransformZ = _playerTransform.transform.position.z;
        _miniMapParam.rocketTransformX = _playerTransform.transform.position.x;
        _token = this.GetCancellationTokenOnDestroy();
        ServiceLocator<UIMediator>.GetInstance().Init(_miniMapParam);
        StartInGameFlow();
    }

    /// <summary>
    /// ゲーム進行フローを非同期で開始し、キャンセル以外の例外をログに出す。
    /// </summary>
    private void StartInGameFlow()
    {
        InGameFlow().Forget(exception =>
        {
            if (exception is OperationCanceledException) return;

            Debug.LogException(exception);
        });
    }

    void Update()
    {
        // 毎フレームミニマップ用のデータを更新して UI に渡す
        _miniMapParam.cameraRotationY = _cameraTransform.transform.eulerAngles.y;
        _miniMapParam.rocketTransformZ = _playerTransform.transform.position.z;
        _miniMapParam.rocketTransformX = _playerTransform.transform.position.x;
        ServiceLocator<UIMediator>.GetInstance().Reload(_miniMapParam);
    }

    /// <summary>
    /// ゲーム開始時のフローを非同期で処理する。
    /// - バナー表示、入力制御、ミッション待機、敵の HP が 0 になるまで待つ。
    /// </summary>
    private async UniTask InGameFlow()
    {
        ServiceLocator<UIMediator>.GetInstance().Init(_bannerParam);
        ServiceLocator<UIMediator>.GetInstance().Show(_bannerParam);
        // catchはせず、finallyで停止解除だけを保証して例外は呼び出し元へ伝える。
        try
        {
            Time.timeScale = 0;
            ServiceLocator<HandlerController>.GetInstance().HandlersDisable();
            await UniTask.Delay(TimeSpan.FromSeconds(_missionDisplayTime), ignoreTimeScale: true,cancellationToken:_token);
        }
        finally
        {
            // 例外やキャンセルが起きてもゲーム全体の停止状態を残さない。
            Time.timeScale = 1;
            ServiceLocator<HandlerController>.GetInstance().HandlersEnable();
        }
        await UniTask.WaitUntil(() =>  _enemy.NowEnemyHp <= 0, cancellationToken: _token);
        // ゲームクリアへ遷移
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("GameClear", 1f, 1f);
    }
}
