using System;
using UnityEngine;

public class ViewBase : MonoBehaviour
{
    [SerializeField]
    protected Canvas canvas;    //生成時にcanvasの状態を決めれる

    public Type GetParamType() => GetUseParamBase().GetType();
    protected virtual ParamBase GetUseParamBase() => new ParamBase();
    public virtual void OnShow<T>(T param) where T : ParamBase { }
    public virtual void OnHide<T>(T param) where T : ParamBase { }
    public virtual void OnReload<T>(T param) where T : ParamBase { }

    public virtual void OnAnimation<T>(T param) where T : ParamBase { }
    public virtual void OnInit<T>(T param) where T : ParamBase
    {
        canvas.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public virtual void OnFinal<T>(T param) where T : ParamBase
    {
        Destroy(gameObject);
    }
}