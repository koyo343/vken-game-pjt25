using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("アイテム設定")]
    public string playerTag = "Player"; // アイテムを起動するオブジェクトのタグ
    public string targetTag = "Enemy";  // 破壊対象のタグ

    [Header("エフェクト設定")]
    public GameObject destructionEffectPrefab; // 破壊時のエフェクト（任意）

    private Camera mainCamera;
    private ScoreDictionaryManager scoreDictionaryManager;

    void Start()
    {
        // メインカメラの参照を取得
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("シーンに 'MainCamera' タグの付いたカメラがありません！");
            enabled = false;
        }
        //ScoreDictionaryManagerを探索
        if (scoreDictionaryManager == null)
        {
            scoreDictionaryManager = FindObjectOfType<ScoreDictionaryManager>();
            if (scoreDictionaryManager == null)
            {
                Debug.LogWarning("Holoxer: ScoreDictionaryManagerがシーンに見つかりません。スコア処理をスキップします。");
            }
        }
    }

    // Is TriggerがONのコライダーとの接触を検出
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーがアイテムに触れたかチェック
        if (other.CompareTag(playerTag))
        {
            DestroyVisibleEnemies();

            // アイテム自身を破壊
            Destroy(gameObject);
        }
    }

    // カメラ内に映っている敵を破壊するメインロジック
    void DestroyVisibleEnemies()
    {
        // 1. シーン内の全ての破壊対象オブジェクトを取得
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        int destroyedCount = 0;

        foreach (GameObject enemy in enemies)
        {
            // 敵にRendererコンポーネントがあることを確認（SpriteRenderer, MeshRendererなど）
            Renderer renderer = enemy.GetComponent<Renderer>();

            if (renderer != null)
            {
                // 2. RendererのisRendererVisible（カメラに映っているか）をチェック
                // Viewport Point方式よりもシンプルで確実性が高い方法です
                if (renderer.isVisible)
                {
                    // 破壊エフェクトの生成（任意）
                    if (destructionEffectPrefab != null)
                    {
                        Instantiate(destructionEffectPrefab, enemy.transform.position, Quaternion.identity);
                    }

                    // 敵を破壊
                    Destroy(enemy);
                    destroyedCount++;
                }
            }
            else
            {
                // Rendererがない場合、カメラに映っているか判定できないため無視
                Debug.LogWarning($"タグ'{targetTag}'のオブジェクト '{enemy.name}' にRendererコンポーネントがありません。", enemy);
            }
        }

        for (int i = 0; i < destroyedCount; i++)
        {
            scoreDictionaryManager.StrToScore("BeatHoloxer");
        }

        Debug.Log($"カメラ内に映っていた {destroyedCount} 体の敵を破壊しました。");
    }
}