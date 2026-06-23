using UnityEngine;

/// <summary>
/// プレイヤーが発する音データ
/// </summary>
[CreateAssetMenu(menuName = "Data/PlayerSound",fileName ="PlayerSoundData")]
public class PlayerSoundData : ScriptableObject
{
    [SerializeField,Header("SE設定")]
    private AudioClip _changeTargetSound;

    [SerializeField]
    private AudioClip _damageSound;

    [SerializeField]
    private AudioClip _healSound;

    [SerializeField]
    private AudioClip _warningSound;

    [SerializeField]
    private AudioClip _rocketFlightSound;
    [SerializeField]
    private AudioClip _boostSound;

    public AudioClip ChangeTargetSound => _changeTargetSound;

    public AudioClip DamageSound => _damageSound;

    public AudioClip HealSound => _healSound;

    public AudioClip WarningSound => _warningSound;

    public AudioClip RocketFlightSound => _rocketFlightSound;

    public AudioClip BoostSound => _boostSound;
}
