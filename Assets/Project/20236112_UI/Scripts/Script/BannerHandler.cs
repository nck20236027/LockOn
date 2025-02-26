using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerHandler
{
    BannerParam bannerParam = new();

    public void ShowMessegeBanner(string titleText,string mainText)
    {
        bannerParam.titleText = titleText;
        bannerParam.mainText = mainText;
        ServiceLocator<UIMediator>.GetInstance().Show(bannerParam);
    }
}