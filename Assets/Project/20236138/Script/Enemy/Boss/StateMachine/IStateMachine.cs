/// <summary>
/// ボス用ステートマシンのインターフェース。
/// - 現在の状態取得、初期化、状態遷移、ライフサイクルメソッドを提供する。
/// </summary>
public interface IMadeStateMachine
{
    public EnemyModeStateBase CurrentState { get; }

    public void Initialize(int initStateType);

    public void ChangeState(int newStateType);

    public void OnEnter();

    // 毎フレーム更新
    public void OnUpdate();

    // 固定フレーム更新
    public void OnFixedUpdate();

    public void OnExit();

}
