using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class TextureSwitcher : MonoBehaviour
{
    public BGMPlayer BGMPlayer;
    public BGMManager BGMManager;
    public GameData_Manager GameData_Manager;
    
    public GameObject BossObject;
    public Image BackGroundObject;

    [System.Serializable]
    public class BackGroundObjectData
    {
        public Image Backobject;
        public string objectName;
        //public Sprite tempSprite;
    }
    public BackGroundObjectData[] BackGroundObjectDatas;



    // 各キャラクターのデータを保持するクラス

    private class CharacterToTextureData
    {
        public Sprite sprite;
        public AudioClip BGM;
        public RuntimeAnimatorController animatorController;
        //public string MaterialDirectory;
        

        public CharacterToTextureData(Sprite sprite, RuntimeAnimatorController controller, AudioClip BGM)
        {
            this.sprite = sprite;
            this.animatorController = controller;
            this.BGM = BGM;
       }
    }

    private class CharacterToBackObjectData
    {
        public string MaterialDirectory;
        public CharacterToBackObjectData(string MaterialDirectory)
        {
            this.MaterialDirectory = MaterialDirectory;
        }
    }

    private Dictionary<string, CharacterToTextureData> TextureData = new Dictionary<string, CharacterToTextureData>();
    private Dictionary<string, CharacterToBackObjectData> BackObjectData = new Dictionary<string, CharacterToBackObjectData>();


    void Awake()
    {
        // 辞書にキャラクターデータを登録
        // 🚨 ここにデバッグログを追加して、ロード結果を確認します 🚨
        Sprite soraTexture = Resources.Load<Sprite>("Materials/Chara/sora/TokinoSora_stand");
        RuntimeAnimatorController soraBossAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/tokino/toki");
        AudioClip soraBGM = Resources.Load<AudioClip>("Materials/BGM/holo");
        //string soraDictionaly = "Materials/Texture/sora/";
        Debug.Log($"[Sora Load] Sprite is null: {soraTexture == null}, BossAnimator is null: {soraBossAnimator == null}, BGM is null: {soraBGM == null}");
        TextureData.Add("ときのそら", new CharacterToTextureData(soraTexture, soraBossAnimator, soraBGM));

        Sprite kenTexture = Resources.Load<Sprite>("Materials/Chara/ken/KenmochiToya_stand");
        RuntimeAnimatorController kenBossAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/kenmochi/ken");
        AudioClip kenBGM = Resources.Load<AudioClip>("Materials/BGM/niji");
        Debug.Log($"[Kenmochi Load] Sprite is null: {kenTexture == null}, BossAnimator is null: {kenBossAnimator == null}, BGM is null: {kenBGM == null}");
        TextureData.Add("剣持刀也", new CharacterToTextureData(kenTexture, kenBossAnimator, kenBGM));

        Sprite mitoTexture = Resources.Load<Sprite>("Materials/Chara/tsukino/TsukinoMito_stand");
        RuntimeAnimatorController mitoBossAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/tsukino/tsuki");
        AudioClip mitoBGM = Resources.Load<AudioClip>("Materials/BGM/niji");
        Debug.Log($"[Mito Load] Sprite is null: {mitoTexture == null}, BossAnimator is null: {mitoBossAnimator == null}, BGM is null: {mitoBGM == null}");
        TextureData.Add("月ノ美兎", new CharacterToTextureData(mitoTexture, mitoBossAnimator, mitoBGM));

        Sprite uruhaTexture = Resources.Load<Sprite>("Materials/Chara/uruha/Ichinose_stand");
        RuntimeAnimatorController uruhaBossAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/ichinose/ichi");
        AudioClip uruhaBGM = Resources.Load<AudioClip>("Materials/BGM/vspo");
        Debug.Log($"[Uruha Load] Sprite is null: {uruhaTexture == null}, BossAnimator is null: {uruhaBossAnimator == null}, BGM is null: {uruhaBGM == null}");
        TextureData.Add("一ノ瀬うるは", new CharacterToTextureData(uruhaTexture, uruhaBossAnimator, uruhaBGM));
        
        Debug.Log($"Loaded {TextureData.Count} texture data.");

        string soraDictionaly = "Materials/Texture/sora/objects/";
        BackObjectData.Add("ときのそら", new CharacterToBackObjectData(soraDictionaly));

        string kenDictionaly = "Materials/Texture/ken/objects/";
        BackObjectData.Add("剣持刀也", new CharacterToBackObjectData(kenDictionaly));

        string mitoDictionaly = "Materials/Texture/tsukino/objects/";
        BackObjectData.Add("月ノ美 dogs", new CharacterToBackObjectData(mitoDictionaly));

        string uruhaDictionaly = "Materials/Texture/uruha/objects/";
        BackObjectData.Add("一ノ瀬うるは", new CharacterToBackObjectData(uruhaDictionaly));

    }

    void Start()
    {
        Debug.Log("TextureSwitcher Start");
        GameData_Manager.CheckNullInstance();
        BGMPlayer.CheckNullBGMInstance();

        if (BossObject == null)
        {
            Debug.LogError("Boss GameObjectがアタッチされていません。");
            return;
        }

        if(BackGroundObject == null)
        {
            Debug.LogError("BackGround GameObjectがアタッチされていません。");
            return;
        }

        foreach(var BOName in BackGroundObjectDatas)
        {
            if(BOName.Backobject == null)
            {
                Debug.LogError($"{BOName.objectName}がアタッチされていません。");
                return;
            }
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

        
        TextureUpdater(selectedCharacter);


    }

    public void TextureUpdater(string selectedCharacter)
    {
        foreach(var BOName in BackGroundObjectDatas)
        {
            if (BackObjectData.ContainsKey(selectedCharacter))
            {
                CharacterToBackObjectData selectedBackObjectData = BackObjectData[selectedCharacter];
                string searchkey = selectedBackObjectData.MaterialDirectory+BOName.objectName;
                if(Resources.Load<Sprite>(searchkey) != null)
                {
                    SpriteRenderer spriteRenderer = BOName.Backobject.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Resources.Load<Sprite>(searchkey);
                    Debug.Log($"{BOName.objectName}を'{selectedCharacter}'textureに更新しました。{searchkey}");
                }
                else
                {
                    Debug.LogError($"{searchkey}が見つかりません。");
                }
            }
        }
       


        // 辞書から対応するビジュアルデータを取得
        if (TextureData.ContainsKey(selectedCharacter))
        {
            CharacterToTextureData selectedTextureData = TextureData[selectedCharacter];

            // Sprite RendererにSpriteを適用:背景
            SpriteRenderer spriteRenderer = BackGroundObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = selectedTextureData.sprite;
                Debug.Log($"背景画像を'{selectedCharacter}'textureに更新しました。");
            }
            else
            {
                Debug.LogError("背景用のobjectにSpriteRendererコンポーネントが見つかりません。");
            }

            // AnimatorにAnimator Controllerを適用
            Animator animator = BossObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController = selectedTextureData.animatorController;
                Debug.Log($"Animatorを適用しました'{selectedTextureData.animatorController.name}'");
            }
            else
            {
                Debug.LogError("Player GameObjectにAnimatorコンポーネントが見つかりません。");
            }

            AudioClip BGM = BGMManager.GetComponent<AudioClip>();
            if (BGM != null)
            {
                BGMManager.BGMtitle = selectedTextureData.BGM;
                Debug.Log($"BGMを更新しました。 ：'{selectedCharacter}'");
            }





            Debug.Log($"Textureを更新しました。 ：'{selectedCharacter}'");
        }
        else
        {
            Debug.LogWarning($"選択されたキャラクター '{selectedCharacter}' のビジュアルデータが見つかりません。");
        }
    }

}
    
