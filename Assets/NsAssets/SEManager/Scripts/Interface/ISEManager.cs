using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISEManager : IServiceClass, IHasListener,IHasSoundPlayerPool
{
    public void PlaySoundToPan(ISpeaker speaker, AudioClip playSound,bool isOverride);
    public void PlaySound(AudioClip playSound,bool isOverride, bool isLoop = false );
    public void SetSEVolume(float value);

    public float MinVolumeValue { get; }
    public float MaxVolumeValue { get; }
}

public interface IHasListener
{
    public IListener Listener { get; }
}
