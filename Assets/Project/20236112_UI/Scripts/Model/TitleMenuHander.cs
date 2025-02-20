using System.Collections.Generic;
using UnityEngine;

public class TitleMenuHander : MonoBehaviour
{
    private TitleInputHandler _titleInputHandler = new();
    private TitleParam _titleParam = new();
    private GameEndParam _gameEndParam = new();

    private CloseEnder _closeEnder;

    private List<ITitleAction> _titleActions = new List<ITitleAction>();
    private List<IQuitAction> _quitActions = new List<IQuitAction>();

    private int _titleCurrentIndex = 0;
    private int _gameEndCurrentIndex = 0;

    private void Awake()
    {
        _titleActions.Add(new GameStart(_titleParam, _titleInputHandler.SetGameStart));
        _titleActions.Add(new OpenGameEnd(_titleParam, _titleInputHandler.SetGameEnd));

        _quitActions.Add(new CloseEnder(_titleParam, _gameEndParam, _titleInputHandler.SetTitle));
        _quitActions.Add(new GameEnder());

        _closeEnder = new CloseEnder(_titleParam, _gameEndParam, _titleInputHandler.SetTitle);

        _titleInputHandler._onStartSubmit = OnTitleSubmit;
        _titleInputHandler._onEndSubmit = OnGameEndSubmit;

        _titleInputHandler._onTitleChoice = TitleChoice;
        _titleInputHandler._onGameEndChoice = GameEndChoice;

        _titleInputHandler.Init();
    }

    private void Start()
    {
        UIMediator.Instance.Init(_titleParam);
        UIMediator.Instance.Init(_gameEndParam);
    }

    public void OnTitleSubmit()
    {
        _titleActions[_titleCurrentIndex].OnTitleAction();
    }

    public void OnGameEndSubmit()
    {
        _quitActions[_gameEndCurrentIndex].OnQuitAction();
    }

    public void TitleChoice(float direction)        //タイトルの選択
    {
        _titleCurrentIndex -= (int)direction;
        if (_titleCurrentIndex > 1)
        {
            _titleCurrentIndex = 1;
            return;
        }
        if (_titleCurrentIndex < 0)
        {
            _titleCurrentIndex = 0;
            return;
        }
        _titleParam.currentIndex = _titleCurrentIndex;
        UIMediator.Instance.Reload(_titleParam);
    }

    public void GameEndChoice(float direction)        //ゲーム終了時の選択
    {
        _gameEndCurrentIndex -= (int)direction;
        if (_gameEndCurrentIndex > 1)
        {
            _gameEndCurrentIndex = 1;
            return;
        }
        if (_gameEndCurrentIndex < 0)
        {
            _gameEndCurrentIndex = 0;
            return;
        }
        _gameEndParam.currentIndex = _gameEndCurrentIndex;
        UIMediator.Instance.Reload(_gameEndParam);
    }

    private void OnDestroy()
    {
        UIMediator.Instance.Final(_titleParam);
        UIMediator.Instance.Final(_gameEndParam);
        _titleInputHandler.Final();
    }
}
