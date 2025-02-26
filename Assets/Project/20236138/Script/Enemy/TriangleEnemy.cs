using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy :EnemyBase,IMoveObjectable
{
    [SerializeField, Header("探知範囲")]
    private float _sreachDistance = 10;
    [SerializeField, Header("自爆するまでの時間")]
    private float _timeDestruction = 4f;
    [SerializeField,Header("追跡時の速度")]
    private float _chaseSpeed = 10;
    private float _nowTimeDestruntion = 0;
    [SerializeField, Header("自爆時の攻撃力")]
    private float _selfDistructionDamage = 10;

    [SerializeField]
    private MoveStatus _moveStatus;
    public override bool GetIsView => _renderer.isVisible;

    public Transform GetPos => transform;

    public Rigidbody GetRigidbody => rb;

    public Vector3 Gettarget => _isTracking ? TargetManager.Instance.GetPlayerPos : Vector3.zero;

    public override void Damage(int damage)
    {
        Destroy(gameObject);
    }

    //コンポーネント
    Renderer _renderer;
    Rigidbody rb;

    private bool _isTracking = false;
    public void SetStatus(float _selfDestructionTime , float _timeDestruction,float _chaseSpeed,MoveStatus status,bool _isTracking)
    {
        this._selfDistructionDamage = _selfDestructionTime;
        this._timeDestruction = _timeDestruction;
        this._chaseSpeed = _chaseSpeed;
        this._moveStatus = status;
        this._isTracking = _isTracking;
    }
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            RocetMove.MoveTarget(this, _moveStatus, _nowTimeDestruntion);
        if ((GetPos.position - Gettarget).sqrMagnitude < Mathf.Pow(_sreachDistance, 2) || _isTracking)
        {
            _isTracking = true;
            _moveStatus.MaxSpeed = _chaseSpeed;
            _nowTimeDestruntion += Time.fixedDeltaTime;
            if (_nowTimeDestruntion > _timeDestruction)
            {
                Destroy(gameObject);
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

    }
}
