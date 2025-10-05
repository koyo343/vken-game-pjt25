using UnityEngine;
using System.Collections;

public class Ken_Skill : CharacterSkill
{
    public float SkillRecastTime = 7.0f;

    // 生成する弾のプレハブ
    public GameObject bulletPrefab;
    // 弾を生成する場所
    public Transform bulletSpawnPoint;
    // 弾の速度
    public float bulletSpeed = 10f;
    
    public override void PerformSkill()
    {
        Debug.Log("ken skill");

        // プレハブと生成場所が設定されているか確認
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instantiateでプレハブをシーン上に生成する
            // Quaternion.identity は「回転なし」を意味します。弾の向きは後からスクリプトで制御します。
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            
            // 生成した弾にアタッチされているRigidbody2Dコンポーネントを取得
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Rigidbody2Dがアタッチされているか確認
            if (rb != null)
            {
                // このスクリプトがアタッチされているキャラクターの向きを取得します。
                // localScale.x が正なら右向き(1)、負なら左向き(-1)と判断します。
                float direction = Mathf.Sign(transform.localScale.x);

                // 向きと速度を使って、弾に力を加えます。
                rb.velocity = new Vector2(direction * bulletSpeed, 0);

                // キャラクターが左を向いている場合 (directionが-1の場合)
                if (direction < 0)
                {
                    // 弾の見た目も左を向くように180度回転させます。
                    bullet.transform.Rotate(0f, 0f, 180f);
                }
            }
            else
            {
                Debug.LogError("Bullet PrefabにRigidbody2Dがアタッチされていません！");
            }
        }
        else
        {
            // どちらかが設定されていない場合は、エラーメッセージをコンソールに表示
            Debug.LogError("Bullet Prefab または Bullet Spawn Point が Ken_Skill スクリプトに設定されていません！");
        }
    }
}