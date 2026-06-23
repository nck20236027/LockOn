using UnityEngine;

/// <summary>
/// 各種ハンドラ（メニュー、ゲームオーバー等）の共通基底クラス。
/// - 有効化 / 無効化の API を抽象メソッドとして提供する。
/// - 継承先が具体的な表示制御を実装する。
/// </summary>
public abstract class BaseHandler : MonoBehaviour
{
    /// <summary>
    /// ハンドラを有効にする処理を実装する。
    /// </summary>
    public abstract void HandlerEnable();

    /// <summary>
    /// ハンドラを無効にする処理を実装する。
    /// </summary>
    public abstract void HandlerDisable();
}
