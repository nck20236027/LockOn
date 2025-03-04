using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerManager : ISoundPlayerManager
{
    private IHasSoundPlayerPool hasSoundPlayerPool;

    public SoundPlayerManager(IHasSoundPlayerPool hasSoundPlayerPool)
    {
        this.hasSoundPlayerPool = hasSoundPlayerPool;
    }

    public void OnPlaySound(AudioClip playSound, bool isOverride,bool isLoop = false)
    {
        ISoundPlayer useSoundPlayer = DequeueUseSoundPlayer(isOverride);

        //該当するプレイヤーが無ければ
        if (useSoundPlayer == null)
        {
            //鳴らさない
            return;
        }

        //音を鳴らす
        useSoundPlayer.OnPlaySound(playSound,isLoop);

        //最後尾に戻す
        hasSoundPlayerPool.SoundPlayerPool.Enqueue(useSoundPlayer);
    }

    public void OnPlaySoundToPan(ISpeaker speaker, AudioClip playSound, bool isOverride = true)
    {
        ISoundPlayer useSoundPlayer = DequeueUseSoundPlayer(isOverride);

        //該当するプレイヤーが無ければ
        if (useSoundPlayer == null)
        {
            //鳴らさない
            return;
        }

        //Panを使用した音を鳴らす
        useSoundPlayer.OnPlaySoundToPan(speaker, playSound);

        //最後尾に戻す
        hasSoundPlayerPool.SoundPlayerPool.Enqueue(useSoundPlayer);
    }


    private ISoundPlayer DequeueUseSoundPlayer(bool isOverride)
    {

        //一番先頭にある使用されていないSoundPlayerの要素番号を取得
        int firstIndex = hasSoundPlayerPool.SoundPlayerPool.FindIndex(soundPlayer =>
        {
            return soundPlayer.IsPlaying == false;
        });

        if (firstIndex >= 0)
        {
            //indexのSoundPlayerを取得
            return hasSoundPlayerPool.SoundPlayerPool.Dequeue(firstIndex);
        }

        if (isOverride == true)
        {
            //強制的に一番最初の要素を取り出して使う
            return hasSoundPlayerPool.SoundPlayerPool.Dequeue();
        }

        //該当しなければnullを返す
        return null;
    }
}
