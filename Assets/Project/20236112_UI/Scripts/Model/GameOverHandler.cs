using UnityEngine;
using System.Collections.Generic;


public class GameOverHandler : BaseHandler
{
    private List<IGameOverAction> _gameOverAction = new();

    //private TitleParam titleParam;
    private GameOverParam _gameOverParam = new();

    private GameOverInputHandler _gameOverInputHandler = new();

    private QuitParam quitParam = new();
    private GameQuiter gameQuiter = new();

    private int _currentIndex;
    [SerializeField]
    private AudioClip _submitSE;

    private void Start()
    {
        _gameOverAction.Add(new GameRestert(_gameOverParam, _gameOverInputHandler.SetReStartInput));
        _gameOverAction.Add(new GameQuiter());

        HandlerEnable();

        //ServiceLocator<UIMediator>.GetInstance().Init(_gameOverParam);
    }

    public void OnGameEndSubmit()
    {
        _gameOverAction[_currentIndex].OnGameOverAction();
        ServiceLocator<SEManager>.GetInstance().PlaySound(_submitSE, true);

    }

    public void GameOverChoice(float direction)
    {
        //Debug.Log(_quitCurrentIndex);
        _currentIndex -= (int)direction;
        if (_currentIndex > 1)
        {
            _currentIndex = 1;
            return;
        }
        if (_currentIndex < 0)
        {
            _currentIndex = 0;
            return;
        }
        _gameOverParam.currentIndex = _currentIndex;
        ServiceLocator<UIMediator>.GetInstance().Reload(_gameOverParam);

    }

    private void OnDestroy()
    {
        HandlerDisable();
        //ServiceLocator<UIMediator>.GetInstance().Final(_gameEndParam);
        Debug.Log("onDestroy");
    }

    public override void HandlerEnable()
    {
        _gameOverInputHandler._onGameOverSubmit += OnGameEndSubmit;
        _gameOverInputHandler._onGameOverChoice += GameOverChoice;

        _gameOverInputHandler.Init();
    }

    public override void HandlerDisable()
    {
        _gameOverInputHandler._onGameOverSubmit -= OnGameEndSubmit;
        _gameOverInputHandler._onGameOverChoice -= GameOverChoice;

        _gameOverInputHandler.Final();
    }
}
