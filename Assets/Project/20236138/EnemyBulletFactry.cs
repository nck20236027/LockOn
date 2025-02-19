using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletFactry
{
    private float _bulletSspeed = 0;
    private int _bulletPowor =0;
    public EnemyBulletFactry(float _bulletSpeed, int _bulletPowor)
    {
        this._bulletSspeed = _bulletSpeed;
        this._bulletPowor = _bulletPowor;
    }

    //public EnemyBullet CreatEnemyBullet(Transform _transform, Quaternion _quaternion)
    //{
    //    EnemyBullet bullet = _transform.gameObject
    //}
}
