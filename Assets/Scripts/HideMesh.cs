using UnityEngine;

public class HideMeshOnStart : MonoBehaviour
{
    void Start()
    {
        // このスクリプトがアタッチされたゲームオブジェクトの
        // MeshRendererコンポーネントを取得
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // もしMeshRendererが存在すれば、無効にする
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }
}