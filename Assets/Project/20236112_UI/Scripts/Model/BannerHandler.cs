using Cysharp.Threading.Tasks;
using UnityEngine;

public class BannerHandler : MonoBehaviour
{
    BannerParam bannerParam = new();

    [SerializeField]
    private string _titleText;
    [SerializeField]
    private string _mainText;



    private void Awake()
    {
        bannerParam.titleText = _titleText;
        bannerParam.mainText = _mainText;
        UIMediator.Instance.Init(bannerParam);
    }

    private async void Start()
    {
    }
}
