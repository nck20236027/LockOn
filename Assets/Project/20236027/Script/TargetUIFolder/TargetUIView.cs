using LitMotion;
using UnityEngine;
using UnityEngine.UI;

//�����ƃ����[�htarget���������Ƃ��̃V���E�ƃn�C�h������@�قƂ�ǌp��
//�f���Q�[�g�Ń��F�N�^�[3�ŕԂ����\�b�h������target�̃|�W�V������Ԃ�
public class TargetUIView : ViewBase
{
    [SerializeField]
    private Image targetUIImage;
    [Header("��]�ݒ�Q���[�v")]
    [SerializeField] private Vector3 _eulerStartPos;      //���b�g���[�V�����������
    [SerializeField] private Vector3 _eulerEndPos;        //���b�g���[�V����
    [SerializeField] private float _eulerMoveUITimer;     //��������������
    [SerializeField] private int _eulerLoopCount;         //���񃋁[�v���邩
    [SerializeField] private LoopType _eulerLoopType;     //�ǂ�ȃ��[�v�����̂�
    [SerializeField] private Ease _eulerEaseType;         //�ǂ�ȓ�����
    [Header("�J���[�ݒ�Q���[�v")]
    [SerializeField] private Color _colorStart;      //���b�g���[�V�����������
    [SerializeField] private Color _colorEnd;        //���b�g���[�V����
    [SerializeField] private float _colorMoveUITimer;     //��������������
    [SerializeField] private int _colorLoopCount;         //���񃋁[�v���邩
    [SerializeField] private LoopType _colorLoopType;     //�ǂ�ȃ��[�v�����̂�
    [SerializeField] private Ease _colorEaseType;         //�ǂ�ȓ�����
    [Header("�g��k���ݒ�Q���[�v")]
    [SerializeField] private Vector3 _ScaleStart;      //���b�g���[�V�����������
    [SerializeField] private Vector3 _scaleEnd;        //���b�g���[�V����
    [SerializeField] private float _scaleMoveUITimer;     //��������������
    [SerializeField] private int _scaleLoopCount;         //���񃋁[�v���邩
    [SerializeField] private LoopType _scaleLoopType;     //�ǂ�ȃ��[�v�����̂�
    [SerializeField] private Ease _scaleEaseType;         //�ǂ�ȓ�����

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
        //gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
    public override void OnShow<T>(T param)
    {
        base.OnShow(param);
        gameObject.SetActive(true);
    }
    public override void OnFinal<T>(T param)
    {
        base.OnFinal(param);
        Destroy(gameObject);
    }
    //public override void OnFinal<T>(T param)
    //{
    //    //View�̍폜�w�߂��󂯂��玩�g���폜����
    //    Destroy(gameObject);
    //}
    public override void OnAnimation<T>(T param)
    {
        Tweens.ImageLoopRotateTween( targetUIImage.rectTransform,_eulerStartPos, _eulerEndPos, _eulerMoveUITimer, _eulerLoopCount, _eulerLoopType, _eulerEaseType,gameObject);
        Tweens.ImageLoopColorTween(targetUIImage, _colorStart, _colorEnd, _colorMoveUITimer, _colorLoopCount, _colorLoopType, _colorEaseType,gameObject);
        Tweens.ImageLoopScaleTween(targetUIImage.rectTransform, _ScaleStart, _scaleEnd, _scaleMoveUITimer, _scaleLoopCount, _scaleLoopType, _scaleEaseType, gameObject);


        //base.OnAnimation<T>(param);
        //LMotion.Create(new Vector3(0, 0, 0), new Vector3(0, 0, 360), 3f)
        //     .WithLoops(-1, LoopType.Restart)
        //     .WithEase(Ease.Linear)
        //     .BindToEulerAngles(transform);

        //LMotion.Create(new Color(0, 255, 0, 0.3f), new Color(0, 255, 0, 0.9f), 1f)
        //    .WithLoops(-1, LoopType.Yoyo)
        //    .WithEase(Ease.Linear)
        //    .BindToColor(targetUIImage.GetComponent<Image>());

        //LMotion.Create(new Vector3(1.5f, 1.5f, 1.5f), new Vector3(0.5f, 0.5f, 0.5f), 1f) // 1�b�����ăX�P�[����ω�
        //    .WithLoops(-1, LoopType.Yoyo)
        //    .WithEase(Ease.Linear)
        //    .BindToLocalScale(transform);// transform.localScale�ɕR�Â�

    }
    public void OnDestroy()
    {
        ServiceLocator<UIMediator>.GetInstance().Final(new TargetUIParam()); //���ׂĂ�view�ɂ���������Ȃ��ƃo�O��
    }
    void Update()
    {
        //�����Ɏ����Ă���Vector���g����target�̏ꏊ��UI��u��
        if (targetPos.GetTarget== null)
        {
            targetUIImage.gameObject.SetActive(false);
            return;
        }
        targetUIImage.transform.position = Camera.main.WorldToScreenPoint(targetPos.GetTarget.GetTransform.position);

        targetUIImage.gameObject.SetActive(targetPos.GetTarget.GetIsView);


    }
}
