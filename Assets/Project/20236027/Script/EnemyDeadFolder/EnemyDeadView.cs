using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadView : ViewBase
{
    [SerializeField] private GameObject enemyExplosionPosition;
    protected override ParamBase GetUseParamBase() => new EnemyDeadParam();

    public override void OnInit<T>(T param)
    {
        base.OnInit(param);
        EnemyDeadParam targetUIParam = param as EnemyDeadParam;

    }
    public override void OnAnimation<T>(T param)
    {
        base.OnAnimation(param);
        EnemyDeadParam deadEffectParam = param as EnemyDeadParam;
        enemyExplosionPosition.SetActive(true);
        GameObject DeadEffectObject = Instantiate(enemyExplosionPosition,deadEffectParam.enemyDeadPosition, enemyExplosionPosition.transform.rotation);
        //enemyExplosionPosition.transform.position = targetUIParam.enemyDeadPosition.position;

        Destroy(DeadEffectObject,1f);

    }
    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new EnemyDeadParam()); //Ç∑Ç◊ÇƒÇÃviewÇ…Ç±ÇÍÇèëÇ©Ç»Ç¢Ç∆ÉoÉOÇÈ
    }

}
