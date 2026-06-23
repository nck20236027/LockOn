/// <summary>
/// プロジェクト共通の静的データを保持するユーティリティクラス。
/// - Editor/ランタイムで共有する簡易な設定値（例: カメラ感度）を保持する。
/// </summary>
public static class GameData 
{
    // カメラ感度（UI 等から参照/保存される）
    public static float cameraSensitivity = 1.0f;
}
