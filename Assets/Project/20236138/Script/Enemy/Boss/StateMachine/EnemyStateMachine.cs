using System.Collections.Generic;

/// <summary>
/// 敵用のステートマシン実装。
/// - 状態の生成は MadeModeStateFactory に委譲する。
/// - 現在の状態オブジェクトを保持し、状態遷移を行うメソッドを提供する。
/// </summary>
public class EnemyStateMachine : IMadeStateMachine
{
    // 状態辞書（キー: 状態ID, 値: 状態オブジェクト）
    private Dictionary<int, EnemyModeStateBase> _stateDic = new ();
    private EnemyModeStateBase _currentState;

    public EnemyModeStateBase CurrentState => _currentState;

    public EnemyStateMachine(CoreEnemy enemy)
    {
        // 各状態を生成して辞書に登録する
        // 数字を直に入れるのをやめる
        _stateDic.Add(0, EnemyModeStateFactory.Create(0, this, enemy));
        _stateDic.Add(1, EnemyModeStateFactory.Create(1, this, enemy));
        _stateDic.Add(2, EnemyModeStateFactory.Create(2, this, enemy));
        _stateDic.Add(3, EnemyModeStateFactory.Create(3, this, enemy));
        _stateDic.Add(4, EnemyModeStateFactory.Create(4, this, enemy));
    }

    /// <summary>
    /// 状態を切り替える。現在の状態の OnExit を呼び、新しい状態の OnEnter を呼ぶ。
    /// </summary>
    public void ChangeState(int changeStateType)
    {
        _currentState.OnExit();

        _currentState = _stateDic[changeStateType];

        _currentState.OnEnter();
    }

    /// <summary>
    /// ステートマシンの初期化（初期状態の決定）。
    /// </summary>
    public void Initialize(int initStateType)
    {
        _currentState = _stateDic[initStateType];
    }

    public void OnEnter()
    {
        _currentState.OnEnter();
    }

    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    public void OnFixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    public void OnExit()
    {
        _currentState.OnExit();
    }
}
