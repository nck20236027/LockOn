using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShereEnemy : EnemyBase
{
    public override bool GetIsView => renderer.isVisible;

    Renderer renderer;
    public override void Damage(int damage)
    {
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnDestroy()
    {
        base.OnDestroy();
        
    }
}
