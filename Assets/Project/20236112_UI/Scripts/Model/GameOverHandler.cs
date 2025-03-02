using UnityEngine;
using System.Collections.Generic;


public class GameOverHandler : MonoBehaviour
{
    private List<IGameOverAction> _gameOverAction = new();

    //private TitleParam titleParam;
    private GameOverParam _gameOverParam = new();

    private GameOverInputHandler _gameOverInputHandler = new();

    private QuitParam quitParam = new();
    private GameQuiter gameQuiter = new GameQuiter();

    private int _currentIndex;

    private void Awake()
    {
        _gameOverAction.Add(new GameRestert(_gameOverParam, _gameOverInputHandler.SetReStartInput));
        _gameOverAction.Add(new GameQuiter());

        //_gameOverParam.currentIndex = 0;

        _gameOverInputHandler._onGameOverSubmit += OnGameEndSubmit;
        //_gameOverInputHandler._onGameOverChoice += ;
        _gameOverInputHandler.Init();

    }

    private void Start()
    {
        ServiceLocator<UIMediator>.GetInstance().Init(_gameOverParam);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServiceLocator<UIMediator>.GetInstance().Show(_gameOverParam);
        }
    }

    public void OnGameEndSubmit()
    {
        _gameOverAction[_currentIndex].OnGameOverAction();
    }

    public void QuitChoice(float direction)
    {
        ////Debug.Log(_quitCurrentIndex);
        //_quitCurrentIndex -= (int)direction;
        //if (_quitCurrentIndex > 1)
        //{
        //    _quitCurrentIndex = 1;
        //    return;
        //}
        //if (_quitCurrentIndex < 0)
        //{
        //    _quitCurrentIndex = 0;
        //    return;
        //}
        //_gameOverParam.currentIndex = _quitCurrentIndex;
        //ServiceLocator<UIMediator>.GetInstance().Reload();

    }

    private void OnDestroy()
    {
        _gameOverInputHandler.Final();
        //ServiceLocator<UIMediator>.GetInstance().Final(_gameEndParam);
        Debug.Log("onDestroy");
    }

}
