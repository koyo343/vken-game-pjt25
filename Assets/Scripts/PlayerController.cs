using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public bool jumpCheck = false;
    private float debugjump = 0;
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
        Walking();

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

    void Walking()
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
        if (Input.GetButtonDown("Jump") && jumpCheck)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCheck = false;
            animator.SetBool("isJumping", true);
        } else if (Input.GetKeyDown(KeyCode.J)) {
            jumpCheck = true;
            debugjump++;
        }

        if (isGrounded)  // 地面にいる場合
        {
            jumpCheck = true;
            animator.SetBool("isJumping", false);
        }
        else  // 空中にいる場合
        {
            if (debugjump == 0)
            {
                jumpCheck = false;
                animator.SetBool("isJumping", true);
            }
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
                 transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (moveInput > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
        if (playerCollider == null)
        {
            isGrounded = false;
            return;
        }

        // レイキャストのパラメータを設定
        Vector2 raycastDirection = Vector2.down;
        float raycastDistance = 0.2f; // 余裕を持たせた距離
        LayerMask mask = ~LayerMask.GetMask("Player");

        // レイキャストの開始位置をコライダーの下端から少し内側にずらす
        float offsetFromEdge = 0.2f;
        Vector2 leftOrigin = new Vector2(playerCollider.bounds.min.x + offsetFromEdge, playerCollider.bounds.min.y);
        Vector2 rightOrigin = new Vector2(playerCollider.bounds.max.x - offsetFromEdge, playerCollider.bounds.min.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, raycastDirection, raycastDistance, mask);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, raycastDirection, raycastDistance, mask);

        isGrounded = (leftHit.collider != null && leftHit.collider.CompareTag("Ground")) ||
                    (rightHit.collider != null && rightHit.collider.CompareTag("Ground"));

        Debug.DrawRay(leftOrigin, raycastDirection * raycastDistance, isGrounded ? Color.green : Color.red);
        Debug.DrawRay(rightOrigin, raycastDirection * raycastDistance, isGrounded ? Color.green : Color.red);
    }
}