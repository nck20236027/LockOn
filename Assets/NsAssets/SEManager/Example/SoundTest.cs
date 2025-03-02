using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour,ISpeaker
{
    [SerializeField]
    private Vector3 speakPos;

    [SerializeField]
    private AudioClip sound;

    public Vector3 SpeakerPos => speakPos;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ServiceLocatorを経由して呼びだす
            ServiceLocator<SEManager>.GetInstance().PlaySoundToPan(this, sound, true);
            ServiceLocator<SEManager>.GetInstance().PlaySound(sound, true);
        }
    }
    //SEはそれぞれ持っていて持っているやつが何かしらのアクションに対してSEmanagerにその音を鳴らしてと頼む

}
