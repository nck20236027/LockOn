using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyBulletStatus
{
    [Header("’e‚Ì‘¬‚³")]
    public float _moveSpeed ;
    [Header("’e‚ÌUŒ‚—Í")]
    public int _bulletPowor ;
    [HideInInspector]
    public float _DestroyTime ;
} 