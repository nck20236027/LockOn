using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using NS;

public class SEManager : ServiceMonoBehaviour<SEManager>, ISEManager
{
    [SerializeField, Header("SEで使用するオーディオミキサー")] 
    private AudioMixerGroup SEAudioMixerGroup;

    [SerializeField, Header("SEで使用するオーディオソースPrefab")]
    private GameObject audioObjectPrefab;

    [SerializeField, Header("音の感知範囲")]
    private float maxHearingDistance = 100.0f; // 最大で聞こえる距離

    [SerializeField, Header("音が一番大きく聞こえる距離")]
    private float minHearingDistance = 1.0f;

    [SerializeField, Header("Panのスケール")]
    private float panScale = 100.0f; // 最大で聞こえる距離

    [SerializeField, Header("音の伝わり型")]
    private AudioRolloffMode rolloffMode = AudioRolloffMode.Linear; // 最大で聞こえる距離

    [SerializeField, Header("オーディオソースの生成数")]
    private int audioCreateCount = 5;

    public IListener Listener => ServiceLocator<IListener>.GetInstance();

    private LinkedQueue<ISoundPlayer> soundPlayerPool = new LinkedQueue<ISoundPlayer>();
    public LinkedQueue<ISoundPlayer> SoundPlayerPool => soundPlayerPool;

    [SerializeField, Header("音量の最小値")]
    private float minvolumeValue = -20f;

    public float MinVolumeValue => minvolumeValue;

    [SerializeField, Header("音量の最大値")]
    private float maxVolumeValue = 20f;

    public float MaxVolumeValue => maxVolumeValue;

    private ISoundPlayerManager soundPlayerManager;

    protected override void Awake()
    {
        base.Awake();
        //poolSize分だけオーディオを生成
        ISEAudio[] seAudios = new ISEAudio[audioCreateCount];

        //生成処理的に同時にやると抜けることがあるため先にオブジェクトの生成処理
        for (int i = 0; i < seAudios.Length; i++)
        {
            seAudios[i] = Instantiate(audioObjectPrefab,transform)?.GetComponent<ISEAudio>();

            //オーディオの初期設定
            seAudios[i].InitializeAudio(minHearingDistance, maxHearingDistance, SEAudioMixerGroup, rolloffMode);
        }

        //生成したAudioを元にsoundPlayerを生成する
        for (int i = 0; i < seAudios.Length; i++)
        {
            //サウンドを鳴らすクラスを生成
            ISEAudio seAudio = seAudios[i];
            if(seAudio != null)
            {
                IVolumeCoordinater volumeCoordinater = new VolumeCoordinator(maxHearingDistance, panScale);     //音量調節クラス
                ISoundPlayer soundPlayer = new SoundPlayer(volumeCoordinater, this, seAudio);                   //サウンドを鳴らす指示を出すクラス

                //追加
                soundPlayerPool.Enqueue(soundPlayer);
            }

        }

        soundPlayerManager = new SoundPlayerManager(this);
    }

    public void SetSEVolume(float value)
    {
        SEAudioMixerGroup.audioMixer
            .SetFloat(SEAudioMixerGroup.name, value);
    }

    public void PlaySound(AudioClip playSound, bool isOverride = true)
    {
        soundPlayerManager.OnPlaySound(playSound, isOverride);
    }

    public void PlaySoundToPan(ISpeaker speaker, AudioClip playSound, bool isOverride = true)
    {
        soundPlayerManager.OnPlaySoundToPan(speaker, playSound, isOverride);
    }
}
