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
        ServiceLocator<UIMediator>.GetInstance().Init(param);

        //レンダラーのisvisible
        
    }

    // Update is called once per frame
    void Update()
    {
        ServiceLocator<UIMediator>.GetInstance().Reload(param);
    }
}
