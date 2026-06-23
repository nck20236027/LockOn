using UnityEngine;

/// <summary>
/// 敵弾（単発）の挙動を表すコンポーネント。
/// - FixedUpdate で移動時間を進め、寿命を超えたら非アクティブ化する。
/// - 衝突時に IDamageable を介してダメージを与える。
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    private float _nowTime = 0;

    public EnemyBulletStatus status;

    public void SetStatus(EnemyBulletStatus value)
    {
        status = value;
    }

    /// <summary>
    /// 弾が再利用された時に寿命タイマーを初期化する。
    /// </summary>
    private void OnEnable()
    {
        _nowTime = 0;
    }

    private void FixedUpdate()
    {
        _nowTime += Time.fixedDeltaTime;
        if (_nowTime >= status.destroyTime)
        {
            Clear();
        }
        transform.position += transform.forward * status.moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(status.bulletPower);
            Clear();
        }
    }

    /// <summary>
    /// 弾を非アクティブ化してプールに戻せる状態にする。
    /// </summary>
    private void Clear()
    {
        _nowTime = 0;
        gameObject.SetActive(false);
    }

}
