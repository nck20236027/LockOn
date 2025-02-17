using NS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasSoundPlayerPool
{
    public LinkedQueue<ISoundPlayer> SoundPlayerPool { get; }
}
