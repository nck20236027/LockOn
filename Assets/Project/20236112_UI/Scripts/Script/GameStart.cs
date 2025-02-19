using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : ITitleAction
{
    public TitleActionType TitleActionType => TitleActionType.GameStart;
    public void OnTitleAction()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("GameScene", 1f, 1f);
    }
}
