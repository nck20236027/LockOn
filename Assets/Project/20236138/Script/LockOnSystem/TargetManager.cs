using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ロックオン対象を管理するシングルトンコンポーネント。
/// - シーン内の ILockTargetable を登録/解除し、カメラ視野内かつ範囲内のターゲット一覧を UI に提供する。
/// - 左右ロック入力でターゲットを切り替え、Player に現在ターゲットを通知する。
/// </summary>
public class TargetManager : MonoBehaviour,IhasTargetPos,ISubTargetUI
{
    [SerializeField]
    private List<ILockTargetable> _targetList = new();

    [SerializeField]
    private Player _player;
    
    [SerializeField]
    private ILockTargetable _target;
    
    [SerializeField]
    private float _searchScope;

    private PlayerAction _inputActions;
    private bool _isInputBound;
    private SubTargetUIParam _subTragetParam;
    private TargetUIParam _targetParam;
    
    public static TargetManager Instance;

    public ILockTargetable GetTarget => _target != null ?  _target : null;
    
    public Vector3 GetPlayerPos => _player.transform.position;

    public List<Vector3> ISubTargetUIList => 
        _targetList.Where(x => IsTargetTerms(x)).Select(x => x.GetTransform.position).ToList();

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

    /// <summary>
    /// 入力イベントとUI表示を初期化する。
    /// </summary>
    private void Start()
    {
        _inputActions = ServiceLocator<PlayerActionManager>.GetInstance().playerAction;
        BindInputActions();

        ServiceLocator<UIMediator>.GetInstance().Init(_targetParam);
        ServiceLocator<UIMediator>.GetInstance().Animation(_targetParam);

        ServiceLocator<UIMediator>.GetInstance().Init(_subTragetParam);
    }

    private void OnDestroy()
    {
        UnbindInputActions();
        if (Instance == this)
        {
            Instance = null;
        }
    }

    /// <summary>
    /// ロックオン切り替え用の入力イベントを登録する。
    /// </summary>
    private void BindInputActions()
    {
        if (_inputActions == null || _isInputBound) return;

        _inputActions.Player.LLock.started += OnLeftLockStarted;
        _inputActions.Player.RLock.started += OnRightLockStarted;
        _isInputBound = true;
    }

    /// <summary>
    /// 登録済みのロックオン入力イベントを解除する。
    /// </summary>
    private void UnbindInputActions()
    {
        if (_inputActions == null || !_isInputBound) return;

        _inputActions.Player.LLock.started -= OnLeftLockStarted;
        _inputActions.Player.RLock.started -= OnRightLockStarted;
        _isInputBound = false;
    }

    /// <summary>
    /// 左方向のロックオン切り替え入力を処理する。
    /// </summary>
    private void OnLeftLockStarted(InputAction.CallbackContext context)
    {
        ChangeTarget(-1);
    }

    /// <summary>
    /// 右方向のロックオン切り替え入力を処理する。
    /// </summary>
    private void OnRightLockStarted(InputAction.CallbackContext context)
    {
        ChangeTarget(1);
    }

    private void Update()
    {
        ServiceLocator<UIMediator>.GetInstance().Reload(_subTragetParam);
    }

    /// <summary>
    /// ターゲットを登録する（ILockTargetable）。主に TargetToken から呼ばれる。
    /// </summary>
    public void AddLockTarget(ILockTargetable target)
    {
        _targetList.Add(target);
    }

    /// <summary>
    /// ターゲットを登録解除する。
    /// </summary>
    public void RemoveLockTarget(ILockTargetable target)
    {
        _targetList.Remove(target);
        if(_target == target)
        {
            _target = null;
            _player.ChangeTarget(null);
        }
    }

    /// <summary>
    /// 左右入力に応じて視界内ターゲットを切り替える。
    /// - cameraInTargets が空なら何もしない。
    /// - 現在ターゲットがある場合は画面 X 座標でソートして前後のターゲットを選ぶ。
    /// </summary>
    private void ChangeTarget(int x)
    {
        List<ILockTargetable> cameraInTargets = 
            _targetList.Where(x => IsTargetTerms(x))
            .ToList();
        if(cameraInTargets.Count <= 0) return;

        if(_target != null)
        {
            cameraInTargets.Sort((a,b) =>(int)(
            Camera.main.WorldToScreenPoint(a.GetTransform.position).x -
            Camera.main.WorldToScreenPoint(b.GetTransform.position).x));
            int targetNo = cameraInTargets.FindIndex(x => _target == x);
            if (targetNo < 0)
            {
                _target = cameraInTargets.First();
                _player.ChangeTarget(_target);
                ServiceLocator<UIMediator>.GetInstance().Animation(_targetParam);
                return;
            }
            _target = cameraInTargets[(targetNo + x + cameraInTargets.Count)%cameraInTargets.Count];
        }
        else
        {
            _target = cameraInTargets.First();
        }
        _player.ChangeTarget(_target);
        ServiceLocator<UIMediator>.GetInstance().Animation(_targetParam);
    }
    /// <summary>
    /// 指定トークンがターゲット条件（視界内かつ範囲内）を満たすか判定する。
    /// </summary>
    private bool IsTargetTerms(ILockTargetable token) 
    {
        Vector2 point = Camera.main.WorldToViewportPoint(token.GetTransform.position);
        
        return token.GetIsView
            && (token.GetTransform.position - _player.transform.position).sqrMagnitude <= Mathf.Pow(_searchScope, 2)
            && point.x > 0
            && point.x < 1
            && point.y > 0
            && point.y < 1;
    }
}
