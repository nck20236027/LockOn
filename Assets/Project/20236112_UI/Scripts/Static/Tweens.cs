using LitMotion;
using UnityEngine;
using UnityEngine.UI;

public static class Tweens
{
    public static MotionHandle TextColorTween(Text tweenText, Color initColor, Color targetColor, float duration, LitMotion.Ease ease, GameObject gameObject)
    {
        return LMotion.Create(initColor, targetColor, duration)
            .WithEase(ease)
            .WithOnComplete(() => tweenText.color = targetColor)
            .Bind(tweenColor => tweenText.color = tweenColor)
            .AddTo(gameObject);
    }

    public static MotionHandle ImageScaleTween(RectTransform tweenTransform, Vector3 initScale, Vector3 targetScale, float duration, LitMotion.Ease ease, GameObject gameObject)
    {
        return LMotion.Create(initScale, targetScale, duration)
            .WithEase(ease)
            .WithOnComplete(() => tweenTransform.localScale = targetScale)
            .Bind(tweenScale => tweenTransform.localScale = tweenScale)
            .AddTo(gameObject);
    }

    public static MotionHandle FontSizeTween(Text tweenText, int initSize, int targetSize, float duration, LitMotion.Ease ease, GameObject gameObject)
    {
        return LMotion.Create(initSize, targetSize, duration)
            .WithEase(ease)
            .WithOnComplete(() => tweenText.fontSize = targetSize)
            .Bind(tweenScale => tweenText.fontSize = tweenScale)
            .AddTo(gameObject);
    }

    public static MotionHandle ImageColorTween(Image tweenImage, Color initImageColor, Color targetImageColor, float duration, LitMotion.Ease ease, GameObject gameObject)
    {
        return LMotion.Create(initImageColor, targetImageColor, duration)
            .WithEase(ease)
            .WithOnComplete(() => tweenImage.color = targetImageColor)
            .Bind(tweenColor => tweenImage.color = tweenColor)
            .AddTo(gameObject);
    }
}
