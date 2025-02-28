using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBulletPool : ServiceMonoBehaviour<EnemyBulletPool>,IServiceClass
{
    [SerializeField]
    private EnemyBullet _bullet;

    [SerializeField,Header("最初に作るオブジェクトの数")]
    int _objectCount = 10; 

    private List<EnemyBullet> _enemyBulletList = new List<EnemyBullet>();
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        for (int i = 0; i <　_objectCount; i++)
        {
            _enemyBulletList.Add(Instantiate(_bullet));

        }

    }

    public GameObject GetBullet(Vector3 _setPosition,Quaternion _rotation,
        EnemyBulletStatus _status)
    {
        EnemyBullet bullet = _enemyBulletList.Where(x=>!x.gameObject.activeSelf).FirstOrDefault();
        if(bullet == null)
        {
            bullet = Instantiate(_bullet);
            _enemyBulletList.Add(bullet);
        }
        bullet.transform.position = _setPosition;
        bullet.transform.rotation = _rotation;
        bullet.SetStatus(_status);
        bullet.gameObject.SetActive(true);
        return bullet.gameObject;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
    }




}
