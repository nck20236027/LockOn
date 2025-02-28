using UnityEngine;
using System.Collections.Generic;


public class GameOverHandler : MonoBehaviour
{
    private TitleParam titleParam;
    private GameOverParam _gameOverParam = new();
    private GameOverInputHandler _gameOverInputHandler;

    private List<IGameOverAction> _gameOverAction = new();
    //private List<IQuitAction> _quitAction = new();

    private int _currentIndex;

    private void Awake()
    {
        _gameOverAction.Add(new GameRestert(_gameOverParam, _gameOverInputHandler.));
    }

    private void Start()
    {
        _gameOverParam.currentIndex = 0;
        ServiceLocator<UIMediator>.GetInstance().Init(_gameOverParam);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServiceLocator<UIMediator>.GetInstance().Show(_gameOverParam);
        }
    }

    public void OnGameOverAction()
    {

    }


    public void OnGameEndSubmit()
    {
        _gameOverAction[_currentIndex].OnGameOverAction();
    }

}
