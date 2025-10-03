using UnityEngine;
using System.Collections;

public class Ken_Skill : CharacterSkill
{
    public float SkillRecastTime = 7.0f;

    // 生成する弾のプレハブ
    public GameObject bulletPrefab;
    // 弾を生成する場所
    public Transform bulletSpawnPoint;
    
    public override void PerformSkill()
    {
        Debug.Log("ken skill");

        // プレハブと生成場所が設定されているか確認
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instantiateでプレハブをシーン上に生成する
            // bulletSpawnPoint.position: 生成する位置
            // bulletSpawnPoint.rotation: 生成する向き
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
        else
        {
            // どちらかが設定されていない場合は、エラーメッセージをコンソールに表示
            Debug.LogError("Bullet Prefab または Bullet Spawn Point が Ken_Skill スクリプトに設定されていません！");
        }

    }
}