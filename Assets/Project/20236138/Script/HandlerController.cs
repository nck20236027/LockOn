using UnityEngine;

/// <summary>
/// 各種 UI ハンドラ（メニュー、ゲームオーバーなど）を管理するコントローラクラス。
/// - Awake/OnDestroy で自身を ServiceLocator に登録・解除する。
/// - 外部から呼ばれるハンドラの有効化/無効化 API を提供する。
/// </summary>
public class HandlerController : MonoBehaviour,IServiceClass
{
    [SerializeField]
    private MenuHandler _menuHandler;
    [SerializeField]
    private GameOverHandler _gameoverHandler;

    private void Awake()
    {
        // ServiceLocator に登録
        ServiceLocator<HandlerController>.Register(this);
    }

    private void OnDestroy()
    {
        // 登録解除
        ServiceLocator<HandlerController>.RemoveInstance(this);
    }

    public void HandlersEnable()
    {
        _menuHandler.HandlerEnable();
    }

    public void HandlersDisable()
    {
        _menuHandler.HandlerDisable();
        _gameoverHandler.HandlerDisable();
    }

    public void GameoverHandler()
    {
        _menuHandler.HandlerDisable();
        _gameoverHandler.HandlerEnable();
    }
}
