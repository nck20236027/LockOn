using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundPlayerManager
{
    public void OnPlaySound(AudioClip playSound, bool isOverride, bool isLoop = false);
    public void OnPlaySoundToPan(ISpeaker speaker, AudioClip playSound, bool isOverride);
}
