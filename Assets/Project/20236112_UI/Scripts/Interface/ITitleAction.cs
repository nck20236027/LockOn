public interface ITitleAction
{
    public TitleActionType TitleActionType { get; }     //Œp³‚É‚Ç‚Ìenum‚©éŒ¾‚·‚é


    public void OnTitleAction();

}
public enum TitleActionType
{
    GameStart,
    GameEnd
}
