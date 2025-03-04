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
//押されれたときに入力の情報を保存＆タイマー開始
//タイマーがゼロになった時に保存した入力を実行    実行後タイマーを再度使用可能になる
//タイマーカウント中に入力の情報の可能