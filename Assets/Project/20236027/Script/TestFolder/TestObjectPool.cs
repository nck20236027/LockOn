using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class TestObjectPool : MonoBehaviour
{
    private List<Image> poolObject = new List<Image>();

    [SerializeField] Image objects;

    void Start()
    {

    }

    public void SetupPool(Image poolObj)
    {
       poolObject.Add(poolObj);//’Ç‰Á
       poolObj.gameObject. SetActive(false);
    }
    public Image GetPool()
    {
        if(poolObject.Count == 0)
        {
            SetupPool(CreatePool());
        }
        Image ObjectPool = poolObject[0];
        ObjectPool .gameObject.SetActive(true);
        poolObject.Remove(ObjectPool);
        return ObjectPool;
    }
    private Image CreatePool()
    {
        Image gameObject = Instantiate(objects);
        gameObject.transform.parent = transform;
        return gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
