using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// OnReload�̒��ŁATween.���\�b�h��(�ω��Ώ�,�ύX�O,�ύX��,�J�ڎ���,ease,gameobject)�ŏ���
/// ease�� [SerializeField] private Ease changeEase = Ease.Linear;�̂悤�Ȋ����Ő錾���Ă�����
/// �F�̕ύX�̏ꍇColor(1,1,1,1)���AColor.white�̂悤�Ȍ`�ŏ����ƒZ���ł���
/// </summary>
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
            .Bind(tweenSize => tweenText.fontSize = tweenSize)
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

    public static MotionHandle ImageLoopRotateTween(RectTransform tweenTransform, Vector3 initRotate, Vector3 targetRotate, float duration, int loops, LoopType loopType, LitMotion.Ease ease, GameObject gameObject)
    {
        return LMotion.Create(initRotate, targetRotate, duration)
            .WithEase(ease)
            .WithLoops(loops, loopType)
            .BindToEulerAngles(tweenTransform)
            .AddTo(gameObject);
    }
    public static MotionHandle ImageLoopScaleTween(RectTransform tweenTransform, Vector3 initRotate, Vector3 targetRotate, float duration, int loops, LoopType loopType, LitMotion.Ease ease, GameObject gameObject)
    {
        return LMotion.Create(initRotate, targetRotate, duration)
            .WithEase(ease)
            .WithLoops(loops, loopType)
            .BindToLocalScale(tweenTransform)
            .AddTo(gameObject);
    }
}
