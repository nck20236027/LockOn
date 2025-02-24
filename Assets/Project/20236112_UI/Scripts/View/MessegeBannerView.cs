using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessegeBannerView : ViewBase
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Color initColor;
    [SerializeField]
    private Color fadeInColor;
    [SerializeField]
    private Color fadeOutColor;

    [SerializeField]
    float direction = 0.1f;

    [SerializeField]
    LitMotion.Ease ease;


    public override void OnInit<T>(T param)
    {
        Tweens.ImageColorTween(image, fadeInColor, fadeOutColor, direction, ease, gameObject);
            
    }

    public override void OnHide<T>(T param)
    {
        canvas.gameObject.SetActive(gameObject);
    } 
    public override void OnFinal<T>(T param)
    {
        Destroy(gameObject);
    }
}
