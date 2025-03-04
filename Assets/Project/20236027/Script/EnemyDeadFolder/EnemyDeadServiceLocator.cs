using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadServiceLocator : MonoBehaviour,IServiceClass
{
    [SerializeField] private GameObject effectObject;
    public void CreateEffect(Vector3 pos) {
        Instantiate(effectObject, pos,Quaternion.identity);
    }
    private void Awake()
    {
        ServiceLocator<EnemyDeadServiceLocator>.Register(this);
    }

    private void OnDestroy()
    {
        ServiceLocator<EnemyDeadServiceLocator>.RemoveInstance(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
