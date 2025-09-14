using UnityEngine;

public class Rijin_movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    public enum Direction { Left, Right };
    
    //Animatorの情報を入れる変数を宣言
    Animator animator;
    private Direction lastDirection;

    //CameraControllerインスタンスの取得
    public CameraController cameraController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // 地面にいない（空中にいる）場合
        if (!isGrounded)
        {
            if (Mathf.Abs(moveInput) > 0) // 空中移動入力がある場合
            {
                if ((moveInput > 0 && lastDirection == Direction.Right) || (moveInput < 0 && lastDirection == Direction.Left))
                {
                    currentMoveSpeed *= 0.5f; // 速度を半分にする
                }
                else
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.8f, rb.linearVelocity.y); // ブレーキ
                    moveInput = 0;
                }
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
        
        // 落下判定メソッドを呼び出す
        Fall();
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

    void Fall()
    {
        // ここは落下したとみなされる値を記述してください
        if (transform.position.y < -1500f)
        {
            // 落下したとみなされるとフラグを立てる
            if (cameraController != null)
            {
                cameraController.FallFlag = true;
            }

            // プレイヤーをセーブポイントに瞬間移動
            if (cameraController != null)
            {
                transform.position = cameraController.SavePoint;
            }
        }
    }
}