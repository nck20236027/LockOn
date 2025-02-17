using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : ISoundPlayer
{
    private ISoundPlayable soundPlayer;
    private IVolumeCoordinater volumeCoordinater;
    private IHasListener hasListener;

    public bool IsPlaying => soundPlayer.IsPlaying();

    public SoundPlayer(IVolumeCoordinater volumeCoordinater, IHasListener hasListener, ISoundPlayable soundPlayer)
    {
        this.soundPlayer = soundPlayer;
        this.volumeCoordinater = volumeCoordinater;
        this.hasListener = hasListener;
    }

    public void OnPlaySound(AudioClip playSound)
    {
        if (CanPlaySound(playSound) == false)
        {
            return;
        }

        soundPlayer.StopSound();
        soundPlayer.PlaySound(playSound);
    }

    public void OnPlaySoundToPan(ISpeaker speaker, AudioClip playSound)
    {
        if (CanPlaySoundToPan(speaker, playSound) == false)
        {
            return;
        }

        try
        {
            float volume = volumeCoordinater.GetAdjustVolume(hasListener.Listener, speaker);
            float pan = volumeCoordinater.GetAdjustPan(hasListener.Listener, speaker);

            soundPlayer.StopSound();
            soundPlayer.PlaySoundToPan(volume, pan, playSound);

        }
        catch
        {
            Debug.LogError($"IListenerのインスタンスが登録されていません");
            throw;
        }
    }

    private bool CanPlaySoundToPan(ISpeaker speaker, AudioClip playSound)
    {
        if (speaker == null)
        {
            Debug.LogError("speakerがセットされていません");
            return false;
        }

        if (playSound == null)
        {
            Debug.LogError("playSoundがセットされていません");
            return false;
        }

        return true;
    }

    private bool CanPlaySound(AudioClip playSound)
    {

        if (playSound == null)
        {
            Debug.LogError("playSoundがセットされていません");
            return false;
        }

        return true;
    }
}
