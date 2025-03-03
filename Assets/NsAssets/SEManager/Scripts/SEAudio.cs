using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SEAudio : MonoBehaviour, ISEAudio
{
    [SerializeField]
    private AudioSource audioSource;

    private float minHearingDistance;
    private float maxHearingDistance;
    private AudioMixerGroup SEAudioMixerGroup;
    private AudioRolloffMode audioRolloffMode;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.minDistance = minHearingDistance;
        audioSource.maxDistance = maxHearingDistance;
        audioSource.outputAudioMixerGroup = SEAudioMixerGroup;
        audioSource.rolloffMode = audioRolloffMode;
    }

    public void PlaySound(AudioClip playClip, bool isLoop = false)
    {
        audioSource.volume = 1;
        audioSource.panStereo = 0;
        audioSource.loop = isLoop;
        if (isLoop)
        {
            audioSource.clip = playClip;
            audioSource.Play();

        }
        else
        {
        audioSource.PlayOneShot(playClip);

        }
    }

    public void PlaySoundToPan(float volume, float pan, AudioClip playClip)
    {
        audioSource.volume = volume;
        audioSource.panStereo = pan;
        audioSource.PlayOneShot(playClip);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public void InitializeAudio(float minHearingDistance, float maxHearingDistance, AudioMixerGroup audioMixerGroup,AudioRolloffMode audioRolloffMode)
    {

        this.SEAudioMixerGroup = audioMixerGroup;
        this.minHearingDistance = minHearingDistance; //一番近く聞こえる最低の範囲
        this.minHearingDistance = maxHearingDistance;　//感知範囲の最大

        //音の減衰モード
        this.audioRolloffMode = audioRolloffMode;
    }

    public void StopSound(AudioClip audio)
    {
        if(audio == audioSource.clip)
        {
            audioSource.Stop();
        }
    }
}
