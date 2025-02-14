public class SettingController
{
    public SettingParam settingParam = new();
    public PauseParam pauseParam = new();

    public void OpenSetting()
    {
        UIMediator.Instance.Show(settingParam);
        UIMediator.Instance.Hide(pauseParam);
    }

    public void CloseSettingMenu()
    {
        UIMediator.Instance.Hide(settingParam);
        UIMediator.Instance.Show(pauseParam);
    }

}