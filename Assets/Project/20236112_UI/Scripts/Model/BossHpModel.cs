using Unity.VisualScripting;
using UnityEngine;

public class BossHpModel : MonoBehaviour
{
    [SerializeField]
    private int _nowHp;
    [SerializeField]
    private int _maxHp;
    [SerializeField]
    private string _name;


    private BossHpBarParam _hpBarParam = new();

    private void Start()
    {
        _hpBarParam.bossNowHp = _maxHp;
        _hpBarParam.bossName = _name;

        ServiceLocator<UIMediator>.GetInstance().Init(_hpBarParam);

    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _nowHp -= 20;
            _hpBarParam.bossNowHp = _nowHp;
            ServiceLocator<UIMediator>.GetInstance().Animation(_hpBarParam);
        }
    }
}
