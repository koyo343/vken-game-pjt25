using UnityEngine;

public class Badmark : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手にGroundタグが付いているとき
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Groundに当たりました");
            // 0.01秒後に消える
            Destroy(gameObject, 0.01f);
        }

        // 衝突した相手にPlayerタグが付いているとき
        if (collision.gameObject.tag == "Player")
        {
            /*
            ここにPlayerがあたったときのスコア処理
            */

            Debug.Log("Playerに当たりました");
            // 0.01秒後に消える
            Destroy(gameObject, 0.01f);
        }
    }
}