using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerContllorer : MonoBehaviour,IServiceClass
{
    [SerializeField]
    private MenuHandler _menuHandler;
    [SerializeField]
    private GameOverHandler _gameoverHandler;

    private void Awake()
    {
        ServiceLocator<HandlerContllorer>.Register(this);
    }

    private void OnDestroy()
    {
        ServiceLocator<HandlerContllorer>.RemoveInstance(this);
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
