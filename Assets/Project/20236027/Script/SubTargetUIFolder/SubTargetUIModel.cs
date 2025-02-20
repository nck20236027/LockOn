using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//オブジェクトが消えたときの処理がわからない
public class SubTargetUIModel : MonoBehaviour,ISubTargetUI
{
    [SerializeField] List<Transform> targetPos;

    public List<Vector3> ISubTargetUIList => targetPos.Select(x => x.position).ToList();
    private SubTargetUIParam param = new SubTargetUIParam();

    // Start is called before the first frame update
    void Start()
    {
        param.subTargetUI = this;
        UIMediator.Instance.Init(param);

        //レンダラーのisvisible
        
    }

    // Update is called once per frame
    void Update()
    {
        UIMediator.Instance.Reload(param);
    }
}
