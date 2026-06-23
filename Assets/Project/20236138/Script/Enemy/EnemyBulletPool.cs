using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 敵弾のプーリングを行うシンプルなプールコンポーネント。
/// - 最初に一定数の弾オブジェクトを生成して保持し、要求時に非アクティブな弾を返す。
/// - 足りない場合は新規生成してリストに追加する。
/// </summary>
public class EnemyBulletPool : MonoBehaviour,IServiceClass
{
    [SerializeField]
    private EnemyBullet _bullet;

    [SerializeField,Header("初期生成数")]
    private int _objectCount = 10; 

    private List<EnemyBullet> _enemyBulletList = new List<EnemyBullet>();
   
    private void Awake()
    {
        ServiceLocator<EnemyBulletPool>.Register(this);
    }

    private void Start()
    {
        for (int i = 0; i < _objectCount; i++)
        {
            _enemyBulletList.Add(Instantiate(_bullet));
        }
    }

    /// <summary>
    /// プール内から使われていないオブジェクトを探しない場合は新たに生成し提供する
    /// </summary>
    /// <param name="setPosition">生成する座標</param>
    /// <param name="rotation">生成時の角度</param>
    /// <param name="status">弾のステータス</param>
    /// <returns></returns>
    public GameObject GetBullet(Vector3 setPosition,Quaternion rotation,EnemyBulletStatus status)
    {
        EnemyBullet bullet = _enemyBulletList.Where(x=>!x.gameObject.activeSelf).FirstOrDefault();
        if(bullet == null)
        {
            bullet = Instantiate(_bullet);
            _enemyBulletList.Add(bullet);
        }
        bullet.transform.position = setPosition;
        bullet.transform.rotation = rotation;
        bullet.SetStatus(status);
        bullet.gameObject.SetActive(true);
        return bullet.gameObject;
    }


    private void OnDestroy()
    {
        ServiceLocator<EnemyBulletPool>.RemoveInstance(this);
    }




}
