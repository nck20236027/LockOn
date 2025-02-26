using UnityEngine;

public class BoostState : ModeStateBase
{
    private float _stateChangedTime = 0;
    private Player _player;
    private PlayerMoveStatus _state;
    public BoostState(IStateMachine _stateMachine, Player player) : base(_stateMachine)
    {
        this._player = player;
        _state = player.BoostState;
    }

    public override ModeStateType StateType => ModeStateType.Boost;
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log(this.ToString());
        _player._cameraController.Interface.CameraChange();
        _player.GetRigidbody.velocity = Vector3.zero;
        Vector3 diffDir = _player.Gettarget != null ?
        (_player.Gettarget - _player.transform.position).normalized :
        Quaternion.LookRotation(_player.transform.up, Vector3.forward) * Vector3.forward; // ターゲットの方向
        _player.transform.rotation = Quaternion.FromToRotation(Vector3.up, diffDir);
        _player._energyGageParam.buttonState = ButtonState._isInputDown;
        _player._energyGageParam.energyTimeLost = _state.FuelConsumptio * Time.deltaTime;
        ServiceLocator<UIMediator>.GetInstance().Reload(_player._energyGageParam);
        _player._energyGageParam.buttonState = ButtonState._isInputNow;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _player.FuelQuantity -= _state.FuelConsumptio * Time.deltaTime;
        if(_stateChangedTime > _player.BoostStateUnChangeTime&& !_player.isBoostButton)
        {
            stateMachine.ChangeState(ModeStateType.Move);
        }
        ServiceLocator<UIMediator>.GetInstance().Reload(_player._energyGageParam);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _stateChangedTime += Time.fixedDeltaTime;
        MoveTarget();
    }

    public override void OnExit()
    {
        base.OnExit();
        _player._cameraController.Interface.CameraChange();
        _stateChangedTime = 0;
        _player._energyGageParam.buttonState = ButtonState._isInputUp;
    }

    private void MoveTarget()
    {
        RocetMove.MoveTarget(_player, _state, _stateChangedTime);
    }
    }
