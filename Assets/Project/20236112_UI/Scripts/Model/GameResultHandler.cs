using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameResultHandler : MonoBehaviour
{
    private GameResultInputHandler _gameResultInputHandler;

    private GameResultParam _gameResultParam = new();
    private GameQuiter _gameQuiter = new();
    private GameResultInputHandler _gameResultHandler = new();
    [SerializeField]
    private AudioClip _submitSE;

    private void Awake()
    {
        _gameResultInputHandler._onGameResultSubmit += OnResultSubmit;

        _gameResultInputHandler.Init();
    }

    public void Start()
    {
        ServiceLocator<UIMediator>.GetInstance().Init(_gameResultParam);
    }

    public void OnResultSubmit()
    {
        
        ServiceLocator<SEManager>.GetInstance().PlaySound(_submitSE, true);
    }

    public void OnDestroy()
    {
        _gameResultHandler.Final();
    }
}
