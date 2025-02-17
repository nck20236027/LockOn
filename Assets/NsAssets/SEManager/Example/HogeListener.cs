using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogeListener : MonoBehaviour,IListener
{
    public Vector3 ListenerPos => transform.position;

    public Vector3 ListenerForword => transform.forward;

    [SerializeField]
    private SEManager sEManager;

    public void Start()
    {
        //サービスロケータに登録
        ServiceLocator<IListener>.Register(this);
    }
}
