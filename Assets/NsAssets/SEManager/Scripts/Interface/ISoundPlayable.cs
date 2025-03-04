using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundPlayable
{
    public void PlaySound(AudioClip playClip,bool isLoop = false);
    public void PlaySoundToPan(float volume, float pan, AudioClip playClip);
    public void StopSound();
    public void StopSound(AudioClip audio);
    public bool IsPlaying();
}
