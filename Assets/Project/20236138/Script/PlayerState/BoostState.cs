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
        _player.cameraController.Interface.CameraChange();
        _player.GetRigidbody.velocity = Vector3.zero;
        Vector3 diffDir = _player.Gettarget != null ?
        (_player.Gettarget.GetTokenPosition - _player.transform.position).normalized :
        Quaternion.LookRotation(_player.transform.up, Vector3.forward) * Vector3.forward; // ターゲットの方向
        _player.transform.rotation = Quaternion.FromToRotation(Vector3.up, diffDir);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _stateChangedTime += Time.deltaTime;
        if(_stateChangedTime > _player.BoostStateUnChangeTime&& !_player.isBoostButton)
        {
            stateMachine.ChangeState(ModeStateType.Move);
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        MoveTarget();
    }

    public override void OnExit()
    {
        base.OnExit();
        _player.cameraController.Interface.CameraChange();
        _stateChangedTime = 0;
    }

    private void MoveTarget()
    {
        RocetMove.MoveTarget(_player, _state, _stateChangedTime);
    }
    }
