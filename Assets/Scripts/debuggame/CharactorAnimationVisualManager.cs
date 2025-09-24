using UnityEngine;
using System.Collections.Generic;

public class CharactorAnimationVisualManager : MonoBehaviour
{
    public AttackManager AtttackManager;
    
    // 各キャラクターのデータを保持するクラス
    private class CharacterVisualData
    {
        public Sprite Charasprite;
        public RuntimeAnimatorController animatorController;
        public Sprite ItemSprite;


        public CharacterVisualData(Sprite sprite, RuntimeAnimatorController controller, Sprite ItemSprite)
        {
            this.sprite = Charasprite;
            this.animatorController = controller;
            this.ItemSprite = ItemSprite;

        }
    }

    

    // キャラクター名とVisualDataを紐付ける辞書
    private Dictionary<string, CharacterVisualData> characterVisuals = new Dictionary<string, CharacterVisualData>();

    // ゲーム内のPlayer GameObjectへの参照
    public GameObject playerObject;
    public GameObject playerItemObject;

    void Awake()
    {
        // 辞書にキャラクターデータを登録
        // 🚨 ここにデバッグログを追加して、ロード結果を確認します 🚨
        Sprite soraSprite = Resources.Load<Sprite>("Materials/Chara/sora/TokinoSora_stand");
        RuntimeAnimatorController soraAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/tokino/toki");
        Sprite soraItem = Resources.Load<Sprite>("Materials/Chara/sora/soraItem");
        Debug.Log($"[Sora Load] Sprite is exist: {soraSprite != null}, Animator is exist: {soraAnimator != null}, ItemSprite is exist: {soraItem != null}");
        characterVisuals.Add("ときのそら", new CharacterVisualData(soraSprite, soraAnimator, soraItem));

        Sprite kenSprite = Resources.Load<Sprite>("Materials/Chara/ken/KenmochiToya_stand");
        RuntimeAnimatorController kenAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/kenmochi/ken");
        Sprite kenItem = Resources.Load<Sprite>("Materials/Chara/ken/kenItem");
        Debug.Log($"[Kenmochi Load] Sprite is exist: {kenSprite != null}, Animator is exist: {kenAnimator != null}, ItemSprite is exist: {kenItem != null}");
        characterVisuals.Add("剣持刀也", new CharacterVisualData(kenSprite, kenAnimator, kenItem));

        Sprite mitoSprite = Resources.Load<Sprite>("Materials/Chara/tsukino/TsukinoMito_stand");
        RuntimeAnimatorController mitoAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/tsukino/tsuki");
        Sprite mitoItem = Resources.Load<Sprite>("Materials/Chara/tsukino/tsukinoItem");
        Debug.Log($"[Mito Load] Sprite is exist: {mitoSprite != null}, Animator is exist: {mitoAnimator != null}, ItemSprite is exist: {mitoItem != null}");
        characterVisuals.Add("月ノ美兎", new CharacterVisualData(mitoSprite, mitoAnimator, mitoItem));

        Sprite uruhaSprite = Resources.Load<Sprite>("Materials/Chara/uruha/Ichinose_stand");
        RuntimeAnimatorController uruhaAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/ichinose/ichi");
        Sprite uruhaItem = Resources.Load<Sprite>("Materials/Chara/uruha/uruhaItem");
        Debug.Log($"[Uruha Load] Sprite is exist: {uruhaSprite != null}, Animator is exist: {uruhaAnimator != null}, ItemSprite is exist: {uruhaItem != null}");
        characterVisuals.Add("一ノ瀬うるは", new CharacterVisualData(uruhaSprite, uruhaAnimator, uruhaItem));
    
        Debug.Log($"Loaded {characterVisuals.Count} character visual data.");
    }

    void Start()
    {
        // Player GameObjectが存在するか確認

        //GameData_Manager.CheckNullInstance();
        Debug.Log("VisualManager Start");
        GameData_Manager.CheckNullInstance();

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
            selectedCharacter = "ときのそら";
        }
        Debug.Log($"選択されたキャラクター: {selectedCharacter}");

        CharaAnimationUpdate(selectedCharacter);

        /*
        // 辞書から対応するビジュアルデータを取得
        if (characterVisuals.ContainsKey(selectedCharacter))
        {
            CharacterVisualData visualData = characterVisuals[selectedCharacter];

            // Sprite RendererにSpriteを適用
            SpriteRenderer playerspriteRenderer = playerObject.GetComponent<SpriteRenderer>();
            if (playerspriteRenderer != null)
            {
                playerspriteRenderer.sprite = visualData.sprite;
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
        }*/
    }

    public void CharaAnimationUpdate(string ChangedCharaName)
    {
        if (playerObject == null)
        {
            Debug.LogError("Player GameObjectがアタッチされていません。");
            return;
        }
        //string selectedCharacter;
        /*
        if (GameData_Manager.Instance.selectedCharacter != null)
        {
            // GameData_Managerから選択されたキャラクター名を取得
            selectedCharacter = GameData_Manager.Instance.selectedCharacter;   
        }
        else
        {   
            selectedCharacter = "ときのそら";
        }
        Debug.Log($"選択されたキャラクター: {selectedCharacter}");
        */


        // 辞書から対応するビジュアルデータを取得
        if (characterVisuals.ContainsKey(ChangedCharaName))
        {
            CharacterVisualData visualData = characterVisuals[ChangedCharaName];

            // Sprite RendererにSpriteを適用
            SpriteRenderer playerspriteRenderer = playerObject.GetComponent<SpriteRenderer>();
            SpriteRenderer playerItemspriteRenderer = playerItemObject.GetComponent<SpriteRenderer>();
            if (playerspriteRenderer != null)
            {
                playerspriteRenderer.sprite = visualData.sprite;
            }
            else
            {
                Debug.LogError("Player GameObjectにSpriteRendererコンポーネントが見つかりません。");
            }

            if(playerItemspriteRenderer != null)
            {
                playerItemspriteRenderer.sprite = visualData.ItemSprite;
            } else {
                Debug.LogError("Player Item GameObjectにSpriteRendererコンポーネントが見つかりません。");
            }

            // AnimatorにAnimator Controllerを適用
            Animator animator = playerObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController = visualData.animatorController;
                Debug.Log($"Animatorを適用しました'{visualData.animatorController.name}'");
            }
            else
            {
                Debug.LogError("Player GameObjectにAnimatorコンポーネントが見つかりません。");
            }

            Debug.Log($"キャラクター '{ChangedCharaName}' のビジュアルを更新しました。");
        }
        else
        {
            Debug.LogWarning($"選択されたキャラクター '{ChangedCharaName}' のビジュアルデータが見つかりません。");
        }
    }
}