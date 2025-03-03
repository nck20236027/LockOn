using System.Collections.Generic;
using UnityEngine;

public class GameResultHandler : MonoBehaviour
{
    private GameResultInputHandler _gameResultInputHandler = new GameResultInputHandler();

    private GameResultParam _gameResultParam = new();

    List<IResultAction> _gameResultActions = new List<IResultAction>();

    private int _resultActionIndex = 0;

    [SerializeField]
    private AudioClip _submitSE;

    private void Awake()
    {
    }

    public void Start()
    {
        _gameResultActions.Add(new GameResultQuiter(_gameResultParam,_gameResultInputHandler.SetResultInput));
        _gameResultInputHandler.onGameResultSubmit += OnResultSubmit;
        _gameResultInputHandler.Init();
        //ServiceLocator<UIMediator>.GetInstance().Init(_gameResultParam);
    }

    public void OnResultSubmit()
    {
        _gameResultActions[_resultActionIndex].OnResultAction();
    }

    private void OnDestroy()
    {
        _gameResultInputHandler.Final();
    }
}
