using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameClearMovieManager : MonoBehaviour, ISpeaker,IListener
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject sceneChangeObject;
    [SerializeField] private ParticleSystem ParticleObject;
    [SerializeField] private float startExplosion;
    [SerializeField] private float endExplosion;
    [SerializeField] private float gameClearPlain;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip subExplosionSound;
    [SerializeField] private Vector3 speakPos;

     


    GameResultParam gameResultParam = new();

    public Vector3 SpeakerPos => speakPos;
    public Vector3 ListenerPos => ListenerPos;

    private void Awake()
    {
        ServiceLocator<IListener>.Register(this);
    }
    async void Start()
    {
        ServiceLocator<UIMediator>.GetInstance().Init(gameResultParam);
        ServiceLocator<SEManager>.GetInstance().PlaySound(explosionSound, true);
        await UniTask.WaitForSeconds(startExplosion);
        ParticleObject.Play();
        ServiceLocator<SEManager>.GetInstance().PlaySound(subExplosionSound, true);
        await UniTask.WaitForSeconds(endExplosion);
        ServiceLocator<SEManager>.GetInstance().PlaySound(explosionSound, true);
        Destroy(cameraObject);
        await UniTask.WaitForSeconds(gameClearPlain);
        ServiceLocator<UIMediator>.GetInstance().Show(gameResultParam);
        Debug.Log($"{ServiceLocator <PlayerActionManager>.GetInstance().playerAction.asset.name}");
        sceneChangeObject.SetActive(true);
    }
}
