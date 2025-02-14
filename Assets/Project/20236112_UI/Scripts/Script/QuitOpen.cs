using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOpen : IMenuAction
{
    QuitParam quitParam;

    public QuitOpen(QuitParam quitParam)
    {
        this.quitParam = quitParam;
    }

    public MenuActionType MenuActionType => MenuActionType.Title;
    public void OnMenuAction()
    {
        UIMediator.Instance.Show(quitParam);
    }
}
