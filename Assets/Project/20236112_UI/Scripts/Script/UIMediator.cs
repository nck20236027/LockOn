using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIMediator : MonoBehaviour
{ 
    private static UIMediator instance;
    public static UIMediator Instance => instance;

    [SerializeField]
    private Canvas canvas;
    public Canvas Canvas { get => canvas; set => canvas = value; }

    [SerializeField]
    private ViewBase[] viewPrefabs;

    private List<ViewBase> viewBaseInstances = new List<ViewBase>();

    private void Awake()
    {

        instance = instance == null ? this : instance;

        //登録されているMediatorのキャンバスがNullなら自身のキャンバスを登録
        if (instance.Canvas == null)
        {
            instance.Canvas = canvas;
        }

        //登録を確認したら
        if (instance == this)
        {
            //送る
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //消す
            Destroy(gameObject);
        }

    }

    private ViewBase InstanceSelector(Type type)
    {
        return viewBaseInstances
            .Where(view => view.GetParamType() == type)
            .FirstOrDefault();
    }

    private ViewBase PrefabSelector(Type type)
    {
        return viewPrefabs
            .Where(view => view.GetParamType() == type)
            .FirstOrDefault();
    }

    public void Init<T>(T param) where T : ParamBase
    {
        var target = PrefabSelector(typeof(T));
        if (target != null)
        {
            //キャンバスにUIを生成
            var targetInstance = Instantiate(target, canvas.transform);

            //UIの初期化処理
            targetInstance.OnInit(param);

            //インスタンスを登録
            viewBaseInstances.Add(targetInstance);
        }
    }

    public void Final<T>(T param) where T : ParamBase
    {
        var target = InstanceSelector(typeof(T));
        if (target != null)
        {
            //インスタンスを削除
            viewBaseInstances.Remove(target);

            //Viewの終了処理を呼ぶ
            Destroy(target);
        }
    }

    public void PerformAction<T>(T param, Action<ViewBase, T> action) where T : ParamBase
    {
        var target = InstanceSelector(typeof(T));
        if (target != null)
        {
            action(target, param);
        }
    }

    public void Show<T>(T param) where T : ParamBase
    {
        PerformAction(param, (target, paramData) => target.OnShow(paramData));
    }

    public void Hide<T>(T param) where T : ParamBase
    {
        PerformAction(param, (target, paramData) => target.OnHide(paramData));
    }

    public void Reload<T>(T param) where T : ParamBase
    {
        PerformAction(param, (target, paramData) => target.OnReload(paramData));
    }

    public void Animation<T>(T param) where T : ParamBase
    {
        PerformAction(param, (target, paramData) => target.OnAnimation(paramData));
    }

}