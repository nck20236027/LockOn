using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVolumeCoordinater
{
    public float GetAdjustVolume(IListener listener,ISpeaker speaker);
    public float GetAdjustPan(IListener listener,ISpeaker speaker);
}
