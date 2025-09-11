using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float smoothing = 0.5f; // カメラの追従速度

    void LateUpdate()
    {
        if (player != null)
        {
            // カメラの位置をプレイヤーの位置に合わせる
            Vector3 newPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothing);
        }
    }
}