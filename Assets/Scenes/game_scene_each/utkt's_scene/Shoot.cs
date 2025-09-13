using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;     // 弾のプレハブ
    public Transform firePoint;         // 弾の発射位置
    public float fireRate = 0.5f;       // 発射間隔（秒）
    private float nextFireTime = 0f;

    void Update()
    {
        // 弾の発射
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    // 弾を発射する関数
    void Shoot()
    {
        // 弾を生成
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}