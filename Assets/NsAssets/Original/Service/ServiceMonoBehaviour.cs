using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceMonoBehaviour<T> : MonoBehaviour, IServiceClass where T : MonoBehaviour,IServiceClass
{

    protected virtual void Awake()
    {
        //シングルトンの処理
        //サービスロケータに自身のインスタンスを登録
        ServiceLocator<T>.Register(this as T);

        //ロケーターに登録されたインスタンスが自分自身であるなら
        if (ServiceLocator<T>.GetInstance() == this)
        {
            //自身を領域に追加
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //そうでなければ削除
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        ServiceLocator<T>.RemoveInstance(this as T);
    }
}
