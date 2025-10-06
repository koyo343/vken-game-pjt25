using UnityEngine;

public class Bullet_chara : MonoBehaviour
{
    public GameObject explosionPrefab;
    private float screenBoundsX = 10f;

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > screenBoundsX)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
            
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}