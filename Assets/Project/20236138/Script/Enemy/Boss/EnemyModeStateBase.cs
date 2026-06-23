/// <summary>
/// ボス用ステートの共通基底クラス。
/// - 各種ステート（Idle/Act1/Act2/Act3/Death）はこのクラスを継承して実装される。
/// - stateMachine を保持し、OnEnter/OnUpdate/OnFixedUpdate/OnExit をオーバーライドする。
/// </summary>
public abstract class EnemyModeStateBase
{
    // ステートマシンの参照
    protected IMadeStateMachine stateMachine;

    public abstract CoreEnemyState StateType { get; }

    public EnemyModeStateBase(IMadeStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public virtual void OnEnter() {}
    public virtual void OnUpdate() {}
    public virtual void OnFixedUpdate() {}
    public virtual void OnExit() {}
}
