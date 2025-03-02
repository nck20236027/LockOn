
using UnityEngine;

public class GameEnder : IQuitAction
{
    public QuitActionType QuitActionType => QuitActionType.Quit;

    public void OnQuitAction()
    {
        Debug.Log("end");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
