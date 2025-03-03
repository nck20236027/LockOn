using System.Collections.Generic;
using UnityEngine;

public class GameResultHandler : MonoBehaviour
{
    private GameResultInputHandler _gameResultInputHandler = new GameResultInputHandler();

    private GameResultParam _gameResultParam = new();
    private GameQuiter _gameQuiter = new();

    List<IResultAction> _gameResultActions = new List<IResultAction>();

    [SerializeField]
    private AudioClip _submitSE;

    private void Awake()
    {
        _gameResultActions.Add(new GameQuiter());
        _gameResultInputHandler.onGameResultSubmit += OnResultSubmit;
        _gameResultInputHandler.Init();
    }

    public void Start()
    {
        ServiceLocator<UIMediator>.GetInstance().Init(_gameResultParam);
    }

    public void OnResultSubmit()
    {
        ServiceLocator<SceneLoader>.GetInstance().LoadScene("TitleScene", 1f, 1f);
        ServiceLocator<SEManager>.GetInstance().PlaySound(_submitSE, true);
    }

    private void OnDestroy()
    {
        _gameResultInputHandler.Final();
    }
}
