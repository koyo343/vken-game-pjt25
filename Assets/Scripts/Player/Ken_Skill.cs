using UnityEngine;
using System.Collections;

public class Ken_Skill : CharacterSkill
{
    public float SkillRecastTime = 7.0f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    
    public override void PerformSkill()
    {
        Debug.Log("ken skill");

        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // ■ Y軸回転を想定した弾の発射 ■

            // 弾を、弾の発射地点(bulletSpawnPoint)の「位置」と「向き」に合わせて生成します。
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 弾を発射地点の向き（右方向）に飛ばします。
                // プレイヤーがY軸で180度回転して左を向いていれば、
                // 子であるbulletSpawnPointのright（右方向）も自動的に左を向いています。
                rb.linearVelocity = bulletSpawnPoint.right * bulletSpeed;
            }
            else
            {
                Debug.LogError("Bullet PrefabにRigidbody2Dがアタッチされていません！");
            }
        }
        else
        {
            Debug.LogError("Bullet Prefab または Bullet Spawn Point が Ken_Skill スクリプトに設定されていません！");
        }
    }
}