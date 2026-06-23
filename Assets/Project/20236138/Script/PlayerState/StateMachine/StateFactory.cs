public static class StateFactory
{
    public static ModeStateBase Create(ModeStateType stateType, StateMachine modeStateContext, Player player)
    {
        switch (stateType)
        {

            case ModeStateType.Move: return new MoveState(modeStateContext, player);

            case ModeStateType.Deceleration: return new DecelerationState(modeStateContext, player);

            case ModeStateType.Boost: return new BoostState(modeStateContext, player);

            case ModeStateType.Death: return new DeathState(modeStateContext, player);
        }

        return null;
    }
}