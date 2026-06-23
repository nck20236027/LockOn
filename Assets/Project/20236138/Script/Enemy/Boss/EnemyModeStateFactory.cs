/// <summary>
/// 敵の状態オブジェクトを生成するファクトリ。
/// stateType に応じて適切な EnemyModeStateBase 派生オブジェクトを返す。
/// </summary>
public static class EnemyModeStateFactory
{
    public static EnemyModeStateBase Create(int stateType, EnemyStateMachine modeStateContext,CoreEnemy enemy)
    {
        switch (stateType)
        {
            case 0:
                return new CoreIdleState(modeStateContext, enemy);
            case 1:
                return new CoreAct1State(modeStateContext,enemy);
            case 2:
                return new CoreAct2State(modeStateContext,enemy);
            case 3:
                return new CoreAct3State(modeStateContext, enemy);
            case 4:
                return new CoreDeathState(modeStateContext, enemy);
        }

        return null;
    }
}