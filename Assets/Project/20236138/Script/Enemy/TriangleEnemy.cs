using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    private int _selfDistructionDamage = 10;
    [SerializeField,Header("爆発の範囲")]
    private float _selfDistructionScale = 10;
    [SerializeField,Header("自爆前に音が出るタイミング")]
    float _selfDistructionSpeed = 10;
    [SerializeField]
    LayerMask _mask;
    [SerializeField]
    AudioClip _allermSound;
    [SerializeField]
    AudioClip _deathSound;
    [SerializeField]
    private MoveStatus _moveStatus;
    private CancellationToken token;

    private EnemyDeadParam _deadParam = new();
    public override bool GetIsView => _renderer.isVisible;

    public Transform GetPos => transform;

    public Rigidbody GetRigidbody => rb;

    public Vector3 Gettarget => TargetManager.Instance.GetPlayerPos ;

    public override void Damage(int damage)
    {
        ServiceLocator<SEManager>.GetInstance().PlaySound(_deathSound, true);
        Destroy(gameObject);
    }

    //コンポーネント
    [SerializeField]
    private Renderer _renderer;
    private Rigidbody rb;

    private bool _isTracking = false;
    public void SetStatus(int _selfDestructionDamage , float _timeDestruction,float _chaseSpeed,MoveStatus status,bool _isTracking)
    {
        this._selfDistructionDamage = _selfDestructionDamage;
        this._timeDestruction = _timeDestruction;
        this._chaseSpeed = _chaseSpeed;
        this._moveStatus = status;
        this._isTracking = _isTracking;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        //base.Start();
        token = this.GetCancellationTokenOnDestroy();
        _deadParam.enemyDeadPosition = transform.position;
        ServiceLocator<UIMediator>.GetInstance().Init(_deadParam);
    }

    // Update is called once per frame
    async void FixedUpdate()
    {
            RocetMove.MoveTarget(this, _moveStatus, _nowTimeDestruntion);
        if ((GetPos.position - Gettarget).sqrMagnitude < Mathf.Pow(_sreachDistance, 2))
        {
            if (!_isTracking)
            {
                try
                {
            Attack();
                await UniTask.Delay(TimeSpan.FromSeconds(_timeDestruction - _selfDistructionSpeed),cancellationToken:token);
                ServiceLocator<SEManager>.GetInstance().PlaySound(_allermSound, true);
                await UniTask.Delay(TimeSpan.FromSeconds(_selfDistructionSpeed), cancellationToken: token);
                Destroy(gameObject);

                }
                catch
                {

                }
            }
        _nowTimeDestruntion += Time.fixedDeltaTime;
        }
    }

    private void Attack()
    {
        _isTracking = true;
        _moveStatus.MaxSpeed = _chaseSpeed;

            
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _selfDistructionScale, Vector3.forward,0.0001f,_mask);
        for (int i = 0; i < hits.Length; i++)
        {
            IDamagable damage = hits[i].transform.GetComponent<IDamagable>();
            if(damage != null)
            {
                damage.Damage(_selfDistructionDamage);
            }
        }
        _deadParam.enemyDeadPosition = transform.position;
        ServiceLocator<UIMediator>.GetInstance().Animation(_deadParam);
    }
}
