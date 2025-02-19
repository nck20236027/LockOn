using LitMotion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//オブジェクトプールを使ってUIを使いまわす
//
public class SubTargetUIView : ViewBase
{
    private Image targetUIImage;
    
    [SerializeField] TestObjectPool _testObjectPool;
    ISubTargetUI subTargetUI;
    protected override ParamBase GetUseParamBase() => new SubTargetUIParam();
    List<GameObject> subTargetUIImage = new();

    //a uiいれた

    public override void OnInit<T>(T param)
    {
        base.OnInit(param);
        SubTargetUIParam subTargetUIParam = param as SubTargetUIParam;
        subTargetUI = subTargetUIParam.subTargetUI;
        subTargetUIImage.Add(_testObjectPool.GetPool());
    }
    public override void OnReload<T>(T param)
    {
        base.OnReload(param);
        for (int i = 0; i < subTargetUI.ISubTargetUIList.Count; ++i)
        {
            //aとsubTargetUIの大きさを比べて 足りない分のオブジェクトを追加　GetPool()
            if (subTargetUIImage.Count < subTargetUI.ISubTargetUIList.Count)
            {
                subTargetUIImage.Add(_testObjectPool.GetPool());
            }
            subTargetUIImage[i].transform.position = Camera.main.WorldToScreenPoint(subTargetUI.ISubTargetUIList[i]);

        }
    }
    public override void OnFinal<T>(T param)
    {
        base.OnFinal(param);

    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
