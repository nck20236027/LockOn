using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public interface ISEAudio : ISoundPlayable
{
    public void InitializeAudio(float minHearingDistance, float maxHearingDistance, AudioMixerGroup audioMixerGroup, AudioRolloffMode audioRolloffMode);
}
