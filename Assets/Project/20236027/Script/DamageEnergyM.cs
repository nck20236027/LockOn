using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;
//ダメージで揺れてもいい
public class DamageEnergyM : MonoBehaviour
{
    [SerializeField] Image greenImage;
    [SerializeField] Image red2Image;
    [SerializeField] GameObject red2;
    [SerializeField] float DamagePoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            red2Image.fillAmount = greenImage.fillAmount;//ここで緑と同じ位置にその次表示
            red2.SetActive(true);
            greenImage.fillAmount -= DamagePoint;//緑がダメージ分食らう
            //非同期処理で　一秒ごに赤が動き出すようにする 多和田に教えてもらう
            LMotion.Create(red2Image.fillAmount, greenImage.fillAmount - 0.01f, 1f)//ここのあたいは演出でかえる
            .WithEase(Ease.OutExpo)
             .WithOnComplete(() => red2.SetActive(false)) // Withはどの順番でも大丈夫
            .BindToFillAmount(red2Image);
        }
    }
}
//全てが動き終わるまでFalseでモーションの一時停止 ～～.PlaybackSpeed = 0f; // 一時停止
//でもこれだと目標が決まったままだから赤のゲージが過去の緑がいたところに止まって終わる
//キャンセルしたら行けるかもしれないけどわからん過ぎる