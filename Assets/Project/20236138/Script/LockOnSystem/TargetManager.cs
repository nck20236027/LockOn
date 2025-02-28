using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetManager : MonoBehaviour,IhasTargetPos,ISubTargetUI
{
    private SubTargetUIParam _subTragetParam;
    private TargetUIParam _targetParam;
    public static TargetManager Instance;
    [SerializeField]
    List<ILockTargetable> targets = new();

    [SerializeField]
    Player _player;
    [SerializeField]
    ILockTargetable target;
    [SerializeField]
    float searchScope;

    PlayerAction inputActions ;

    public ILockTargetable GetTarget => target != null ?  target : null;
    public Vector3 GetPlayerPos => _player.transform.position;

    public List<Vector3> ISubTargetUIList => 
        targets.Where(x => IsTargetTerms(x)).Select(x => x.GetTokenPosition).ToList();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);

        _targetParam = new();
        _targetParam.targetPos = this;

        _subTragetParam = new SubTargetUIParam();
        _subTragetParam.subTargetUI = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        inputActions = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        inputActions.Enable();
        inputActions.Player.LLock.started += (x) => ChangeTarget(-1);
        inputActions.Player.RLock.started += (x) => ChangeTarget(1);

        ServiceLocator<UIMediator>.GetInstance().Init(_targetParam);
        ServiceLocator<UIMediator>.GetInstance().Animation(_targetParam);

        ServiceLocator<UIMediator>.GetInstance().Init(_subTragetParam);
    }

    private void OnDestroy()
    {
        inputActions?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ServiceLocator<UIMediator>.GetInstance().Reload(_subTragetParam);
    }

    //ターゲットに登録するメソッド
    public void AddLockTarget(ILockTargetable target)
    {
        targets.Add(target);
    }

    //ターゲットに登録解除するメソッド
    public void RemoveLockTarget(ILockTargetable target)
    {
        targets.Remove(target);
        if(this.target == target)
        {
            this.target = null;
            _player.ChangeTarget(null);
        }
    }

    //ロックするターゲットを変更
    //元のターゲットがいない場合一番左
    private void ChangeTarget(int x)
    {
        List<ILockTargetable> cameraInTargets = 
            targets.Where(x => IsTargetTerms(x))
            .ToList();
        if(cameraInTargets.Count <= 0) return;
        if(target != null)
        {
        cameraInTargets.Sort((a,b) =>(int)(
        Camera.main.WorldToScreenPoint(a.GetTokenPosition).x -
        Camera.main.WorldToScreenPoint(b.GetTokenPosition).x));
        int targetNo = cameraInTargets.FindIndex(x => target == x);
        target = cameraInTargets[(targetNo + x + cameraInTargets.Count)%cameraInTargets.Count];
        }
        else
        {
            target = cameraInTargets.First();
        }
        _player.ChangeTarget(target);
        ServiceLocator<UIMediator>.GetInstance().Animation(_targetParam);
    }
    //ターゲットをロックする条件
    private bool IsTargetTerms(ILockTargetable token) => token.GetIsView
        && (token.GetTokenPosition - _player.transform.position).sqrMagnitude <= Mathf.Pow(searchScope, 2);
}
