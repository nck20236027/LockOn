using System.Collections.Generic;

/// <summary>
/// - ModeStateType に基づいて各ステートを生成し、遷移を行う。
/// - プレイヤー側と同様に OnUpdate/OnFixedUpdate を委譲する。
/// </summary>
public class StateMachine : IStateMachine
{
    private Dictionary<ModeStateType, ModeStateBase> _stateDic = new ();
    private ModeStateBase _currentState;
    
    public ModeStateBase CurrentState => _currentState;

    public StateMachine(Player player)
    {
        _stateDic.Add(ModeStateType.Deceleration, StateFactory.Create(ModeStateType.Deceleration,this,player));
        _stateDic.Add(ModeStateType.Move, StateFactory.Create(ModeStateType.Move, this, player));
        _stateDic.Add(ModeStateType.Boost, StateFactory.Create(ModeStateType.Boost, this, player));
        _stateDic.Add(ModeStateType.Death, StateFactory.Create(ModeStateType.Death, this, player));
    }

    public void ChangeState(ModeStateType changeStateType)
    {
        _currentState.OnExit();

        _currentState = _stateDic[changeStateType];

        _currentState.OnEnter();
    }

    public void Initialize(ModeStateType initStateType)
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