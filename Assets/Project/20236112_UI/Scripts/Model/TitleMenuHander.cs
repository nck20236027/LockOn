using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuHander : MonoBehaviour
{
    private TitleInputHandler titleInputHandler = new();
    private TitleParam _titleParam = new();

    private List<ITitleAction> titleActions = new List<ITitleAction>();

    private int _currentIndex = 0;

    private void Awake()
    {
        titleInputHandler.Init();
        titleActions.Add(new GameStart());
        titleActions.Add(new OpenGameEnd(_titleParam, titleInputHandler.OnGameEndInputEneble, titleInputHandler.OnTitleInputEneble));

        titleInputHandler._onStartSubmit = OnTitleSubmit;


    }

    public void OnTitleSubmit()
    {
        Debug.Log("TitleSubmit");

        titleActions[_currentIndex].OnTitleAction();
    }


    public void TitleChoice(float direction)
    {
        _currentIndex -= (int)direction;
        if (_currentIndex > 1)
        {
            _currentIndex = 1;
            return;
        }
        if (_currentIndex > 0)
        {
            _currentIndex = 0;
            return;
        }
        _titleParam.currentIndex = _currentIndex;
        UIMediator.Instance.Reload(_titleParam);
    }

    //public void EndChoice(float direction)        ////ƒQ[ƒ€I—¹Žž‚Ì‘I‘ð
    //{
    //    _currentIndex -= (int)direction;
    //    if (_currentIndex > 1)
    //    {
    //        _currentIndex = 1;
    //        return;
    //    }
    //    if (_currentIndex > 0)
    //    {
    //        _currentIndex = 0;
    //        return;
    //    }
    //    //_titleParam.currentIndex = _currentIndex;
    //    UIMediator.Instance.Reload(_titleParam);
    //}
}
