using UnityEngine;
using System.Collections;

public class Uruha_Skill : CharacterSkill
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public GameObject currentBoss;
    public float bulletSpeed = 10f;

    public override void PerformSkill()
    {
        Debug.Log("uruha skill");

        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // 弾を、弾の発射地点(bulletSpawnPoint)の「位置」と「向き」に合わせて生成
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (transform.localScale.x < 0)
            {
                Quaternion currentRotation = bullet.transform.rotation;
                Quaternion NewRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z + 180f);
                bullet.transform.rotation = NewRotation;
            }
            if (rb != null)
            {
                // プレイヤーの向き（localScale.x）に応じて速度を設定
                rb.linearVelocity = new Vector2(transform.localScale.x * bulletSpeed, 0);
            }
            
            // 生成した弾からBulletコンポーネント取得
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            // 取得したコンポーネントの isPenetrating フラグを true に設定
            if (bulletComponent != null)
            {
                // この弾を「貫通弾」に設定する
                bulletComponent.isPenetrating = true;

                if(currentBoss != null)
                {
                    // 弾にボスの情報を渡す
                    bulletComponent.boss = currentBoss;
                }
            }
        }
        else
        {
            Debug.LogError("Bullet Prefab または Bullet Spawn Point が Uruha_Skill スクリプトに設定されていません！");
        }
    }
}