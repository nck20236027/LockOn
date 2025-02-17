using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundPlayer
{
    public bool IsPlaying { get; }
    public void OnPlaySoundToPan(ISpeaker speaker, AudioClip playSound);
    public void OnPlaySound(AudioClip playSound);
}
