using UnityEngine;
using System.Collections.Generic;

public class CharactorAnimationVisualManager : MonoBehaviour
{
    // 各キャラクターのデータを保持するクラス
    private class CharacterVisualData
    {
        public Sprite sprite;
        public RuntimeAnimatorController animatorController;

        public CharacterVisualData(Sprite sprite, RuntimeAnimatorController controller)
        {
            this.sprite = sprite;
            this.animatorController = controller;
        }
    }

    // キャラクター名とVisualDataを紐付ける辞書
    private Dictionary<string, CharacterVisualData> characterVisuals = new Dictionary<string, CharacterVisualData>();

    // ゲーム内のPlayer GameObjectへの参照
    public GameObject playerObject;

    void Awake()
    {
        // 辞書にキャラクターデータを登録
        // 🚨 ここにデバッグログを追加して、ロード結果を確認します 🚨
        Sprite soraSprite = Resources.Load<Sprite>("Materials/Chara/sora/TokinoSora_stand");
        RuntimeAnimatorController soraAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/tokino/toki");
        Debug.Log($"[Sora Load] Sprite is null: {soraSprite == null}, Animator is null: {soraAnimator == null}");
        characterVisuals.Add("ときのそら", new CharacterVisualData(soraSprite, soraAnimator));

        Sprite kenSprite = Resources.Load<Sprite>("Materials/Chara/ken/KenmochiToya_stand");
        RuntimeAnimatorController kenAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/kenmochi/ken");
        Debug.Log($"[Kenmochi Load] Sprite is null: {kenSprite == null}, Animator is null: {kenAnimator == null}");
        characterVisuals.Add("剣持刀也", new CharacterVisualData(kenSprite, kenAnimator));

        Sprite mitoSprite = Resources.Load<Sprite>("Materials/Chara/tsukino/TsukinoMito_stand");
        RuntimeAnimatorController mitoAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/tsukino/tsuki");
        Debug.Log($"[Mito Load] Sprite is null: {mitoSprite == null}, Animator is null: {mitoAnimator == null}");
        characterVisuals.Add("月ノ美兎", new CharacterVisualData(mitoSprite, mitoAnimator));

        Sprite uruhaSprite = Resources.Load<Sprite>("Materials/Chara/uruha/Ichinose_stand");
        RuntimeAnimatorController uruhaAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/ichinose/ichi");
        Debug.Log($"[Uruha Load] Sprite is null: {uruhaSprite == null}, Animator is null: {uruhaAnimator == null}");
        characterVisuals.Add("一ノ瀬うるは", new CharacterVisualData(uruhaSprite, uruhaAnimator));
        
        Debug.Log($"Loaded {characterVisuals.Count} character visual data.");
    }

    void Start()
    {
        // Player GameObjectが存在するか確認

        //GameData_Manager.CheckNullInstance();

        if (playerObject == null)
        {
            Debug.LogError("Player GameObjectがアタッチされていません。");
            return;
        }
        string selectedCharacter;

        if (GameData_Manager.Instance.selectedCharacter != null)
        {
            // GameData_Managerから選択されたキャラクター名を取得
            selectedCharacter = GameData_Manager.Instance.selectedCharacter;   
        }
        else
        {   
            selectedCharacter = "剣持刀也";
        }
        Debug.Log($"選択されたキャラクター: {selectedCharacter}");


        // 辞書から対応するビジュアルデータを取得
        if (characterVisuals.ContainsKey(selectedCharacter))
        {
            CharacterVisualData visualData = characterVisuals[selectedCharacter];

            // Sprite RendererにSpriteを適用
            SpriteRenderer spriteRenderer = playerObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = visualData.sprite;
            }
            else
            {
                Debug.LogError("Player GameObjectにSpriteRendererコンポーネントが見つかりません。");
            }

            // AnimatorにAnimator Controllerを適用
            Animator animator = playerObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController = visualData.animatorController;
                Debug.Log("Animatorを適用しました'{visualData.animatorController.name}'");
            }
            else
            {
                Debug.LogError("Player GameObjectにAnimatorコンポーネントが見つかりません。");
            }

            Debug.Log($"キャラクター '{selectedCharacter}' のビジュアルを更新しました。");
        }
        else
        {
            Debug.LogWarning($"選択されたキャラクター '{selectedCharacter}' のビジュアルデータが見つかりません。");
        }
    }
}