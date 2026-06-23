
public interface IStateMachine
{
    /// <summary>
    /// 現在のステート
    /// </summary>
    public ModeStateBase CurrentState { get; }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="initStateType"></param>
    public void Initialize(ModeStateType initStateType);

    /// <summary>
    /// ステート変更
    /// </summary>
    /// <param name="newStateType"></param>
    public void ChangeState(ModeStateType newStateType);

    /// <summary>
    /// ステート開始時の処理
    /// </summary>
    public void OnEnter();

    /// <summary>
    /// ステート内 Update
    /// </summary>
    public void OnUpdate();

    /// <summary>
    /// ステート内 FixedUpdate
    /// </summary>
    public void OnFixedUpdate();

    /// <summary>
    /// ステート終了時の処理
    /// </summary>
    public void OnExit();

}
