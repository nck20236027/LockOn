using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenGameEnd : ITitleAction
{
    private TitleParam titleParam;

    public Action<bool> onGameEndInputEneble;
    public Action<bool> onTitleInputEneble;

    public TitleActionType TitleActionType => TitleActionType.GameEnd;

    public OpenGameEnd(TitleParam titleParam, Action<bool> onGameEndInputEneble, Action<bool> onTitleInputEneble)
    {
        this.titleParam = titleParam;
        this.onGameEndInputEneble = onGameEndInputEneble;
        this.onTitleInputEneble= onTitleInputEneble;
    }

    public void OnTitleAction()
    {


    }
}
