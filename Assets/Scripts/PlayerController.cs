using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    public enum Direction { Left, Right };

    //Animatorの情報を入れる変数を宣言
    Animator animator;
    private Direction lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //変数にAnimatorの情報を取得して入れる
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        // 左右の移動入力を取得
        float moveInput = Input.GetAxis("Horizontal");

        // 地面にいるかどうかに応じて、移動速度を調整
        float currentMoveSpeed = moveSpeed;

        //最後に入力した左右キーを保持
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            lastDirection = Direction.Left;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            lastDirection = Direction.Right;
        }

        if (!isGrounded) // 地面にいない（空中にいる）場合
        {
            if ((moveSpeed > 0 && lastDirection == Direction.Left) || (moveSpeed > 0 && lastDirection == Direction.Right))
            {
                currentMoveSpeed *= 0.5f; // 速度を半分にする
            }
            else
            {
                moveInput = 0;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
        }

        // プレイヤーの速度を更新
        rb.linearVelocity = new Vector2(moveInput * currentMoveSpeed, rb.linearVelocity.y);

        // isWalking の判定
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.01f && isGrounded);


        // ジャンプ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (isGrounded)  // 地面にいる場合
        {
            animator.SetBool("isJumping", false);
        }
        else  // 空中にいる場合
        {
            animator.SetBool("isJumping", true);
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

    // 地面判定
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