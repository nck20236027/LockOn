using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetManager : MonoBehaviour,IhasTargetPos
{
    private TargetUIParam param;
    public static TargetManager Instance;
    [SerializeField]
    List<ILockTargetable> targets = new();

    [SerializeField]
    Player player;
    [SerializeField]
    ILockTargetable target;
    [SerializeField]
    float searchScope;

    PlayerAction inputActions ;

    public Vector3 GetPos => target != null ?  target.GetTokenPosition : Vector3.zero;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);

        param = new();
        param.targetPos = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        inputActions = new();
        inputActions.Enable();
        inputActions.Player.LLock.started += (x) => ChangeTarget(-1);
        inputActions.Player.RLock.started += (x) => ChangeTarget(1);

        UIMediator.Instance.Init(param);
        UIMediator.Instance.Animation(param);
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        player.ChangeTarget(target);
    }
    //ターゲットをロックする条件
    private bool IsTargetTerms(ILockTargetable token) => token.GetIsView
        && (token.GetTokenPosition - player.transform.position).sqrMagnitude <= Mathf.Pow(searchScope, 2);
}
