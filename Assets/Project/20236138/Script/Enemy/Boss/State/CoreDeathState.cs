/// <summary>
/// ボスの Death（死亡）状態。
/// - 現状はプレースホルダ的な実装。必要に応じて演出や破片生成を追加する。
/// </summary>
public class CoreDeathState : EnemyModeStateBase
{
    public CoreDeathState(IMadeStateMachine stateMachine,CoreEnemy enemy) : base(stateMachine){}
    
    public override CoreEnemyState StateType => CoreEnemyState.Death;
}
