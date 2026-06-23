/// <summary>
/// カメラアニメーションAPIを提供するためのインターフェース
/// </summary>
public interface ICameraControllable
{
    public void CameraShake();
    public void CameraChange();

    public void CameraStop();

}
