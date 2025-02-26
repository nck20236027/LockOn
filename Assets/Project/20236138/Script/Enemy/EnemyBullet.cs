using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public EnemyBulletStatus status;
    private float _nowTime = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        _nowTime += Time.fixedDeltaTime;
        if (_nowTime >= status._bulletPowor)
        {
            Clear();
        }
        transform.position += transform.forward * status._moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.Damage(status._bulletPowor);
        }
    }

    private void Clear()
    {
        _nowTime = 0;
        gameObject.SetActive(false);
    }
}
