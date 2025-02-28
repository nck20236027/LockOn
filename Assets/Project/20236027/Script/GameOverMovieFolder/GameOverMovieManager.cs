using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameOverMovieManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private ParticleSystem ParticleObject;
    [SerializeField] private float startExplosion;
    [SerializeField] private float endExplosion;

    async void Start()
    {
        await UniTask.WaitForSeconds(startExplosion);
        ParticleObject.Play();
        await UniTask.WaitForSeconds(endExplosion);
        Destroy(cameraObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
