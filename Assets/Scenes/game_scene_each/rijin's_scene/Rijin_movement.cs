using UnityEngine;

public class Rijin_movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public float jumpCount = 1;
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

    void FixedUpdate()
    {
        //移動メソッドの呼び出し
        isWalking();

        //地面判定チェックメソッドの呼び出し
        GroundCheck();

    }
    void Update()
    {     
        //ジャンプメソッドの呼び出し
        Jump();

        //方向メソッドを呼び出す
        PlayerDirection();

        // 落下判定メソッドを呼び出す
        FallCheck();
    }

    void isWalking()
    {
        // 左右の移動入力を取得
        float moveInput = Input.GetAxis("Horizontal");

        // 地面にいる場合のみ、左右の移動を適用
        if (isGrounded)
        {
            // プレイヤーの速度を更新
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            //地上にいる間はプレイヤーの最後の入力方向を更新する
            if (moveInput >= 0)
            {
                lastDirection = Direction.Right;
            }
            else
            {
                lastDirection = Direction.Left;
            }
        }
        else if (lastDirection == Direction.Right && moveInput < 0)
        {
            // プレイヤーの速度を更新
            rb.linearVelocity = new Vector2(moveInput * moveSpeed * -0.3f, rb.linearVelocity.y);
        }
        else if (lastDirection == Direction.Left && moveInput > 0)
        {
            // プレイヤーの速度を更新
            rb.linearVelocity = new Vector2(moveInput * moveSpeed * -0.3f, rb.linearVelocity.y);
        }
        else
        {
            // プレイヤーの速度を更新
            rb.linearVelocity = new Vector2(moveInput * moveSpeed * 0.5f, rb.linearVelocity.y);
        }

        // isWalking の判定
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.01f && isGrounded);
    }

    void Jump()
    {
        // ジャンプ
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount--;
            animator.SetBool("isJumping", true);
        }

        if (isGrounded)  // 地面にいる場合
        {
            jumpCount = 1;
            animator.SetBool("isJumping", false);
        }
        else  // 空中にいる場合
        {
            animator.SetBool("isJumping", true);
        }
    }

    void PlayerDirection()
    {
        // 左右の移動入力を取得
        float moveInput = Input.GetAxis("Horizontal");

        // 地面にいる場合プレイヤーの向きを更新
        if (isGrounded)
        {
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

    void FallCheck()
    {
        // プレイヤーがカメラに映らなくなったら
        if (!GetComponent<Renderer>().isVisible)
        {
            if (cameraController != null)
            {
                cameraController.FallFlag = true;
                transform.position = cameraController.SavePoint;
                Debug.Log("Player is fall!!");
            }
        }
    }

        void GroundCheck()
    {
        // BoxCollider2Dが取得できない場合を考慮
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
        if (playerCollider == null) return;

        Vector2 raycastOrigin = new Vector2(transform.position.x, playerCollider.bounds.min.y);
        Vector2 raycastDirection = Vector2.down;
        float raycastDistance = 0.1f;
        
        // プレイヤーレイヤーを無視するレイヤーマスク
        LayerMask mask = ~LayerMask.GetMask("Player");

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance, mask);
        
        // デバッグログで詳細な情報を確認
        if (hit.collider != null)
        {
            Debug.Log("衝突したオブジェクト: " + hit.collider.gameObject.name);
            Debug.Log("衝突したオブジェクトのタグ: " + hit.collider.tag);
        }
        
        // 接地判定
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
        Vector2 raycastOrigin = new Vector2(transform.position.x, playerCollider.bounds.min.y);
        Vector2 raycastDirection = Vector2.down;
        float raycastDistance = 10f;

        // レイキャストが当たっているかどうかで色を変える
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            Gizmos.color = Color.green; // 地面に当たったら緑色
        }
        else
        {
            Gizmos.color = Color.red; // 当たらなかったら赤色
        }

        // レイを描画
        Gizmos.DrawRay(raycastOrigin, raycastDirection * raycastDistance);
    }
}