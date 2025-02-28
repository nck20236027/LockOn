using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameOverInputHandler : MonoBehaviour
{

    private PlayerAction _playerActions;

    public Action _onGameOverSubmit;
    public Action<float> _onGameOverChoice;

    public void Init()
    {
        _playerActions = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        _playerActions.GameOver.Submit.performed += OnGameOverSubmit;
        _playerActions.GameOver.Navigate.performed += OnGameOverSelectX;
    }

    public void Final()
    {
        _playerActions.GameOver.Submit.performed -= OnGameOverSubmit;
        _playerActions.GameOver.Navigate.performed -= OnGameOverSelectX;
    }

    public void OnGameOverSubmit(InputAction.CallbackContext context)        //ゲーム終了の決定
    {
        _onGameOverSubmit();
    }

    public void OnGameOverSelectX(InputAction.CallbackContext context)     //ゲーム終了時の選択入力
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x == 0) { return; }
        float directionX = Mathf.Sign(input.x);
        _onGameOverChoice(directionX);
    }


}
