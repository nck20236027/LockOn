using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TestObjectPool : MonoBehaviour
{
    private List<GameObject> poolObject = new List<GameObject>();
    // 既にプールに入っている既存のアイテムを返そうとすると例外が発生します

    [SerializeField] GameObject objects;

    void Start()
    {

    }

    public void SetupPool(GameObject poolObj)
    {
       poolObject.Add(poolObj);//追加

    }
    public GameObject GetPool()
    {
        if(poolObject.Count == 0)
        {
            SetupPool(CreatPool());
        }
        GameObject ObjectPool = poolObject[0];
        ObjectPool.SetActive(true);
        poolObject.Remove(ObjectPool);
        return ObjectPool;
    }
    private GameObject CreatPool()
    {
        GameObject gameObject = Instantiate(objects);
        gameObject.transform.parent = transform;
        return gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
