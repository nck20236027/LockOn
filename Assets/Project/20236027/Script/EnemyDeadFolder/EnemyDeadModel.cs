using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadModel : MonoBehaviour
{   
    [SerializeField] private Transform deadPosition;

    private EnemyDeadParam enemyDeadParam = new EnemyDeadParam();
    void Start()
    {

            //enemyDeadParam.enemyDeadPosition.position = deadPosition.position;
            ServiceLocator<UIMediator>.GetInstance().Init(enemyDeadParam);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            enemyDeadParam.enemyDeadPosition = deadPosition.position;
            ServiceLocator<UIMediator>.GetInstance().Animation(enemyDeadParam);
        }
    }
}
