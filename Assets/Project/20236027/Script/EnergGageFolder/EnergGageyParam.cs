
public class EnergyGageParam : ParamBase
{

    public ButtonState buttonState;
    public float maxEnergyGauge;
    public float nowEnergyGauge;

    public float energyTimeLost;
    public bool isDamage;
    public float damageEnergyPoint;

}
public enum ButtonState
{
    _isInputDown,
    _isInputNow,
    _isInputUp,
    Non,
}

