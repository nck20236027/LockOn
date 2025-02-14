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
//        //オプションのみ閉じる
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
//        //メニューごと閉じる
//    }
//}

//public interface IOptionOpenable
//{
//    //このインターフェースを継承してるやつは、OpenOptionを絶対に実装する。
//    public void OpenOption();
//}

//public class OptionParam : ParamBase
//{
//    public Action onCloseOption;
//}
