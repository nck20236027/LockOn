using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuitAction
{
    public QuitActionType QuitActionType { get; }
    public void OnQuitAction();
}

public enum QuitActionType
{
    BackMenu,
    Title,
}