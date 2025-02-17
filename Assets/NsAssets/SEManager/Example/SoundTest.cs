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

}
