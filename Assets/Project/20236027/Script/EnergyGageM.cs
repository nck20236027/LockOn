using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;
//ここには緑ゲージの減少も書かれている（常時のほう）
//ブースト中の処理がある
public class EnergyGageM : MonoBehaviour
{
    [SerializeField] Image greenImage;
    [SerializeField] GameObject red;
    [SerializeField] Image redImage;
    [SerializeField] float lostEnergy;      //常に減っている  あとでマイナスにしておくことわかりずらい
    void Start()
    {
        //var token = this.GetCancellationTokenOnDestroy();
        //red2.transform.localScale = new Vector3(1,12,1);
        //await UniTask.WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        LMotion.Create(redImage.fillAmount, greenImage.fillAmount, 1f)
            .WithEase(Ease.OutExpo)
            .BindToFillAmount(redImage);
    }

    //呼ばれたときに緑のゲージと同じ大きさになって非表示から表示になる
    //ダメージの時の処理はディレイをかけて動かす　そのあと非表示
    //ブースト中は呼ばれたときに緑のゲージと同じ大きさになって非表示から表示になる
    //ブーストが終わってから、ちょっとディレイを掛けて緑のゲージと同じ大きさになって非表示

    async void Boost()
    {
        //redImage.fillAmount = greenImage.fillAmount;
    }

    void Update()
    {
        greenImage.fillAmount -= lostEnergy;
        //energyGage -= 0.0001f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            redImage.fillAmount = greenImage.fillAmount;//ここで緑と同じ位置にその次表示
            red.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            greenImage.fillAmount -= lostEnergy; //二倍　今はテスト
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LMotion.Create(redImage.fillAmount, greenImage.fillAmount-0.01f, 1f)//ここのあたいは演出でかえる
            .WithEase(Ease.OutExpo)
             .WithOnComplete(() => red.SetActive(false)) // Withはどの順番でも大丈夫
            .BindToFillAmount(redImage);

            //Boost();
        }
    }
}
//red2.SetActive(true);
//LMotion.Create(new Vector3(1, energyGage, 1), new Vector3(1, holdEnergyGage, 1), 1f)
//.WithEase(Ease.OutExpo)
//.BindToLocalScale(red.transform);// transform.localScaleに紐づけ
