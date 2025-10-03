using UnityEngine;
using System.Collections;

public class Uruha_Skill : CharacterSkill
{
    public float SkillRecastTime = 10.0f;

    // 生成する弾のプレハブ
    public GameObject bulletPrefab;
    // 弾を生成する場所
    public Transform bulletSpawnPoint;

    public override void PerformSkill()
    {
        Debug.Log("uruha skill");

        // プレハブと生成場所が設定されているか確認
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // 1. Instantiateでプレハブをシーン上に生成し、そのインスタンスを newBullet 変数に格納
            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            
            // 2. 生成した弾からBulletコンポーネントを取得
            Bullet bulletComponent = newBullet.GetComponent<Bullet>();

            // 3. 取得したコンポーネントの isPenetrating フラグを true に設定
            if (bulletComponent != null)
            {
                // この弾を「貫通弾」に設定する
                bulletComponent.isPenetrating = true;
            }
        }
        else
        {
            Debug.LogError("Bullet Prefab または Bullet Spawn Point が Uruha_Skill スクリプトに設定されていません！");
        }
    }
}