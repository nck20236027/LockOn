using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceMonoBehaviour<T> : MonoBehaviour, IServiceClass where T : MonoBehaviour,IServiceClass
{

    protected virtual void Awake()
    {
        // サービスロケーターに自身を登録
        ServiceLocator<T>.Register(this as T);

        // 登録したインスタンスが自身であれば、シーンを跨いで破壊されないようにする
        if (ServiceLocator<T>.GetInstance() == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        // サービスロケーターから自身を削除
        ServiceLocator<T>.RemoveInstance(this as T);
    }
}
