using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    public enum Direction { Left, Right };
    
    Animator animator;
    private Direction lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        // ゲームがクリア状態ではない場合のみ、操作を許可する
        if (!GameManager.isGameClear)
        {
            // 左右の移動入力を取得
            float moveInput = Input.GetAxis("Horizontal");

            // ... (既存の移動・ジャンプ処理) ...

            // プレイヤーの速度を更新
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
            
            // isWalking の判定
            animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.01f && isGrounded);

            // ジャンプ
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            
            // プレイヤーの向きを更新
            if (moveInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (moveInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    // 地面判定のメソッドは変更なし
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
