using UnityEngine;

public class MoveFloorController : MonoBehaviour
{
    private float minYPosition;
    private float maxYPosition;
    private float standardPosition;
    private float moveSpeed = 5f;
    private Rigidbody2D rb;

    private int Direction = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        standardPosition = transform.position.y;
        minYPosition = standardPosition - 5f;
        maxYPosition = standardPosition + 5f;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void FixedUpdate()
    {
        // 床を現在の移動方向に沿って動かす
        rb.linearVelocity = new Vector2(0, moveSpeed * Direction);

        // 床の現在のY座標を取得
        float currentYPosition = transform.position.y;

        //上限や下限に来たら動きを反転
        if (currentYPosition >= maxYPosition)
        {
            Direction = -1;
        }
        // 下限に達したら上向きに反転
        else if (currentYPosition <= minYPosition)
        {
            Direction = 1;
        }
    }
}