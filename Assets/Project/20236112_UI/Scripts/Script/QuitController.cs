using UnityEngine;

public class QuitController : MonoBehaviour
{
    public QuitParam quitParam = new();
    public PauseParam pauseParam = new();

    public void OpenQuit()
    {
        UIMediator.Instance.Show(quitParam);
        UIMediator.Instance.Hide(pauseParam);
    }

    public void CloseQuit()
    {
        UIMediator.Instance.Hide(quitParam);
        UIMediator.Instance.Show(pauseParam);
    }
}
