using System;
using Unity.VisualScripting;
using UnityEngine;

public class ViewBase: MonoBehaviour
{
    [SerializeField]
    protected Canvas canvas;    //¶¬Žž‚Écanvas‚Ìó‘Ô‚ðŒˆ‚ß‚ê‚é

    public virtual Type GetParamType() => GetUseParamBase().GetType();
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