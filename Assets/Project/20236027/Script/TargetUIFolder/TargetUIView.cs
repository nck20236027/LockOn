using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

//�����ƃ����[�htarget���������Ƃ��̃V���E�ƃn�C�h������@�قƂ�ǌp��
//�f���Q�[�g�Ń��F�N�^�[3�ŕԂ����\�b�h������target�̃|�W�V������Ԃ�
public class TargetUIView : ViewBase
{
    [SerializeField]
    private Image targetUIImage;
    IhasTargetPos targetPos;
    protected override ParamBase GetUseParamBase() => new TargetUIParam();


    public override void OnInit<T>(T param)
    {
        base.OnInit<T>(param);
        TargetUIParam targetUIParam = param as TargetUIParam;
        targetPos = targetUIParam.targetPos;
    }
    public override void OnHide<T>(T param)
    {
        base.OnHide(param);
    }
    public override void OnShow<T>(T param)
    {
        base.OnShow(param); 
    }
    public override void OnFinal<T>(T param)
    {
        base.OnFinal(param);
    }
    //public override void OnFinal<T>(T param)
    //{
    //    //View�̍폜�w�߂��󂯂��玩�g���폜����
    //    Destroy(gameObject);
    //}
    public override void OnAnimation<T>(T param)
    {
        base.OnAnimation<T>(param);
        LMotion.Create(new Vector3(0, 0, 0), new Vector3(0, 0, 360), 3f)
             .WithLoops(-1, LoopType.Restart)
             .WithEase(Ease.Linear)
             .BindToEulerAngles(transform);

        LMotion.Create(new Color(0, 255, 0, 0.3f), new Color(0, 255, 0, 0.9f), 1f)
            .WithLoops(-1, LoopType.Yoyo)
            .WithEase(Ease.Linear)
            .BindToColor(targetUIImage.GetComponent<Image>());

        LMotion.Create(new Vector3(1.5f, 1.5f, 1.5f), new Vector3(0.5f, 0.5f, 0.5f), 1f) // 1�b�����ăX�P�[����ω�
            .WithLoops(-1, LoopType.Yoyo)
            .WithEase(Ease.Linear)
            .BindToLocalScale(transform);// transform.localScale�ɕR�Â�

    }
    void Update()
    {
        //�����Ɏ����Ă���Vector���g����target�̏ꏊ��UI��u��
        
        targetUIImage.transform.position = Camera.main.WorldToScreenPoint(targetPos.GetPos);


    }
}
