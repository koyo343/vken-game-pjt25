using UnityEngine;

public class Shoot_chara : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (transform.localScale.x < 0){
            Quaternion currentRotation = bullet.transform.rotation;
            Quaternion NewRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z + 180f);
            bullet.transform.rotation = NewRotation;
        }
        if (rb != null)
        {
            // プレイヤーの向き（localScale.x）に応じて速度を設定
            rb.linearVelocity = new Vector2(transform.localScale.x * bulletSpeed, 0);
        }
    }
}