using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameClearMovieManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private ParticleSystem ParticleObject;
    [SerializeField] private float startExplosion;
    [SerializeField] private float endExplosion;
    [SerializeField] private float gameClearPlain;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip subExplosionSound;

    GameResultParam gameResultParam = new();

    async void Start()
    {
        ServiceLocator<SEManager>.GetInstance().PlaySound(explosionSound, true);
        await UniTask.WaitForSeconds(startExplosion);
        ParticleObject.Play();
        ServiceLocator<SEManager>.GetInstance().PlaySound(subExplosionSound, true);
        await UniTask.WaitForSeconds(endExplosion);
        //ServiceLocator<SEManager>.GetInstance().PlaySound(explosionSound, true);
        Destroy(cameraObject);
        await UniTask.WaitForSeconds(gameClearPlain);
        ServiceLocator<UIMediator>.GetInstance().Init(gameResultParam);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
