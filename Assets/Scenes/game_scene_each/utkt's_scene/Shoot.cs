using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Update()
    {
        // ゲームがクリア状態ではない場合のみ、以下の処理を実行する
        if (!GameManager.isGameClear)
        {
            // Fキーが押され、かつ発射レートの条件を満たしている場合
            if (Input.GetKeyDown(KeyCode.F) && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}