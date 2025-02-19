using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy :EnemyBase,IMoveObjectable
{
    [SerializeField, Header("探知範囲")]
    private float _sreachDistance = 10;
    [SerializeField, Header("自爆するまでの時間")]
    private float _timeDestruction = 4f;
    private float _nowTimeDestruntion = 0;

    [SerializeField]
    private MoveStatus _moveStatus;
    public override bool GetIsView => renderer.isVisible;

    public Transform GetPos => transform;

    public Rigidbody GetRigidbody => rb;

    public Vector3 Gettarget => TargetManager.Instance.GetPlayerPos;

    public override void Damage(int damage)
    {
        Destroy(gameObject);
    }

    //コンポーネント
    Renderer renderer;
    Rigidbody rb;

    [SerializeField]
    MoveState moveState;

    private bool _isTracking = false;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((GetPos.position - Gettarget).sqrMagnitude < Mathf.Pow(_sreachDistance, 1) || _isTracking)
        {
            _isTracking = true;  
            _nowTimeDestruntion += Time.fixedDeltaTime;
            RocetMove.MoveTarget(this, _moveStatus, _nowTimeDestruntion);
            if (_nowTimeDestruntion > _timeDestruction)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void OnDestroy()
    {
        base.OnDestroy();

    }
}
