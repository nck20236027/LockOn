using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : ServiceMonoBehaviour<SceneLoader>
{
    [SerializeField,Header("fadeする画像")]
    private Image fadeImage;

    [SerializeField,Header("Canvas")]
    private Canvas fadeCanvas;

    [SerializeField,Header("フェードにかかる時間")]
    private float fadeDuration = 1f;

    private bool isTransitioning = false;

    //シーンをフェード付きでロード
    public void LoadScene(string sceneName, float fadeOutTime, float fadeInTime)
    {
        //シーン遷移中でなければ
        if (!isTransitioning)
        {
            StartCoroutine(Transition(sceneName, fadeOutTime,fadeInTime));
        }
    }

    //フェード,シーン遷移メソッド
    private IEnumerator Transition(string sceneName, float fadeOutTime,float fadeInTime)
    {
        isTransitioning = true;

        fadeImage.raycastTarget = true;
        //ServiceLocator<PlayerActionManager>.GetInstance().Disable();
        //フェードアウト（画面を黒くする）
        yield return FadeImage(1f,fadeOutTime);

        //シーンを非同期でロード
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //フェードイン（画面を元に戻す）
        yield return FadeImage(0f,fadeInTime);
        //ServiceLocator<PlayerActionManager>.GetInstance().Enable();
        isTransitioning = false; //シーン遷移が終わったらフラグを戻す
    }

    //フェード処理（透明度を変える）
    private IEnumerator FadeImage(float targetAlpha,float fadeDuration)
    {
        isTransitioning = true;

        //現在のアルファ値を取得
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            //a値をLeapさせる
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }

        //フェードが終わった状態をセット
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);

        isTransitioning = false;

        fadeImage.raycastTarget = false;
    }

    public bool IsSceneTransitioning() => isTransitioning;
}
