using UnityEngine;

public class PauseController
{
    public PauseParam pauseParam = new();
    public SettingParam settingParam = new();
    //public SettingParam pauseSetting = new();

    //private IOptionOpenable opitionControl; 

    //public PauseController()
    //{
    //    opitionControl = new OptionControlB();
    //}



    public void Resame()
    {
        UIMediator.Instance.Hide(pauseParam);
    }

    //public void ShowMenu()
    //{
    //    UIMediator.Instance.Hide(pauseParam);
    //}

    //public void QuitMenu()
    //{
    //    UIMediator.Instance.Hide(pauseParam);
    //}
}

//public class OptionControlA : IOptionOpenable
//{
//    private OptionParam optionParam = new();

//    public void OpenOption()
//    {
//        optionParam.onCloseOption = CloseOption;
//        UIMediator.Instance.Show(optionParam);
//    }

//    private void CloseOption()
//    {
//        //�I�v�V�����̂ݕ���
//    }
//}


//public class OptionControlB : IOptionOpenable
//{
//    private OptionParam optionParam = new();

//    public void OpenOption()
//    {
//        optionParam.onCloseOption = CloseAll;
//        UIMediator.Instance.Show(optionParam);
//    }

//    private void CloseAll()
//    {
//        //���j���[���ƕ���
//    }
//}

//public interface IOptionOpenable
//{
//    //���̃C���^�[�t�F�[�X���p�����Ă��́AOpenOption���΂Ɏ�������B
//    public void OpenOption();
//}

//public class OptionParam : ParamBase
//{
//    public Action onCloseOption;
//}
