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

    public GameObject holo;
    public GameObject niji;
    public GameObject vspo;

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
        public GameObject Backobject;

        public CharacterToBackObjectData(string MaterialDirectory, GameObject Backobject)
        {
            this.MaterialDirectory = MaterialDirectory;
            this.Backobject = Backobject;
        }
    }

    private Dictionary<string, CharacterToTextureData> TextureData = new Dictionary<string, CharacterToTextureData>();
    private Dictionary<string, CharacterToBackObjectData> BackObjectData = new Dictionary<string, CharacterToBackObjectData>();


    void Awake()
    {
        // 辞書にキャラクターデータを登録
        // 🚨 ここにデバッグログを追加して、ロード結果を確認します 🚨
        Sprite soraTexture = Resources.Load<Sprite>("Materials/BackGroundImage/holo");
        RuntimeAnimatorController soraBossAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/yagoo/yagoo");
        AudioClip soraBGM = Resources.Load<AudioClip>("Materials/BGM/holo");
        //string soraDictionaly = "Materials/Texture/sora/";
        Debug.Log($"[Sora Load] Sprite is exist: {soraTexture != null}, BossAnimator is exist: {soraBossAnimator != null}, BGM is exist: {soraBGM != null}");
        Debug.Log($"Sora's Texture is exist: {(soraTexture != null) & (soraBossAnimator != null) & (soraBGM != null)}");
        TextureData.Add("ときのそら", new CharacterToTextureData(soraTexture, soraBossAnimator, soraBGM));

        Sprite kenTexture = Resources.Load<Sprite>("Materials/BackGroundImage/niji");
        RuntimeAnimatorController kenBossAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/devi/devi");
        AudioClip kenBGM = Resources.Load<AudioClip>("Materials/BGM/niji");
        Debug.Log($"[Kenmochi Load] Sprite is exist: {kenTexture != null}, BossAnimator is exist: {kenBossAnimator != null}, BGM is exist: {kenBGM != null}");
        Debug.Log($"Kenmochi's Texture is exist: {(kenTexture != null) & (kenBossAnimator != null) & (kenBGM != null)}");
        TextureData.Add("剣持刀也", new CharacterToTextureData(kenTexture, kenBossAnimator, kenBGM));

        Sprite mitoTexture = Resources.Load<Sprite>("Materials/BackGroundImage/niji");
        RuntimeAnimatorController mitoBossAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/devi/devi");
        AudioClip mitoBGM = Resources.Load<AudioClip>("Materials/BGM/niji");
        Debug.Log($"[Mito Load] Sprite is exist: {mitoTexture != null}, BossAnimator is exist: {mitoBossAnimator != null}, BGM is exist: {mitoBGM != null}");
        Debug.Log($"Mito's Texture is exist: {(mitoTexture != null) & (mitoBossAnimator != null) & (mitoBGM != null)}");
        TextureData.Add("月ノ美兎", new CharacterToTextureData(mitoTexture, mitoBossAnimator, mitoBGM));

        Sprite uruhaTexture = Resources.Load<Sprite>("Materials/BackGroundImage/vspo");
        RuntimeAnimatorController uruhaBossAnimator = Resources.Load<RuntimeAnimatorController>("Materials/Animator/reid/reid");
        AudioClip uruhaBGM = Resources.Load<AudioClip>("Materials/BGM/vspo");
        Debug.Log($"[Uruha Load] Sprite is exist: {uruhaTexture != null}, BossAnimator is exist: {uruhaBossAnimator != null}, BGM is exist: {uruhaBGM != null}");
        Debug.Log($"Uruha's Texture is exist: {(uruhaTexture != null) & (uruhaBossAnimator != null) & (uruhaBGM != null)}");
        TextureData.Add("一ノ瀬うるは", new CharacterToTextureData(uruhaTexture, uruhaBossAnimator, uruhaBGM));

        Debug.Log($"Loaded {TextureData.Count} texture data.");

        string soraDictionaly = "Materials/Texture/holo/objects/";
        BackObjectData.Add("ときのそら", new CharacterToBackObjectData(soraDictionaly, holo));

        string kenDictionaly = "Materials/Texture/niji/objects/";
        BackObjectData.Add("剣持刀也", new CharacterToBackObjectData(kenDictionaly, niji));

        string mitoDictionaly = "Materials/Texture/niji/objects/";
        BackObjectData.Add("月ノ美兎", new CharacterToBackObjectData(mitoDictionaly, niji));

        string uruhaDictionaly = "Materials/Texture/vspo/objects/";
        BackObjectData.Add("一ノ瀬うるは", new CharacterToBackObjectData(uruhaDictionaly, vspo));

    }

    void Start()
    {
        Debug.Log("TextureSwitcher Start");
        GameData_Manager.CheckNullInstance();
        BGMPlayer.CheckNullBGMInstance();

        AllBackObjectsetFalse();

        if (BossObject == null)
        {
            Debug.LogError("Boss GameObjectがアタッチされていません。");
            //return;
        }

        if (BackGroundObject == null)
        {
            Debug.LogError("BackGround GameObjectがアタッチされていません。");
            //return;
        }

        foreach (var BOName in BackGroundObjectDatas)
        {
            if (BOName.Backobject == null)
            {
                Debug.LogError($"{BOName.objectName}がアタッチされていません。");
                //return;
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


        BGMManager.updateBGMtitle(TextureData[selectedCharacter].BGM);
        BGMManager.callPlayBGM();

        TextureUpdater(selectedCharacter);


    }

    public void TextureUpdater(string selectedCharacter)
    {
        Debug.Log("TextureUpdater is called.");
        if (BackObjectData.ContainsKey(selectedCharacter))
        {
            CharacterToBackObjectData selectedBackObjectData = BackObjectData[selectedCharacter];

            if (selectedBackObjectData.Backobject != null)
            {
                AllBackObjectsetFalse();
                selectedBackObjectData.Backobject.SetActive(true);
                Debug.Log($"背景オブジェクトをアクティブにしました。{selectedCharacter}");
            }
            else
            {
                foreach (var BOName in BackGroundObjectDatas)
                {
                    string searchkey = selectedBackObjectData.MaterialDirectory + BOName.objectName;
                    if (Resources.Load<Sprite>(searchkey) != null)
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
        }



        // 辞書から対応するビジュアルデータを取得
        if (TextureData.ContainsKey(selectedCharacter))
        {
            Debug.Log("test");
            CharacterToTextureData selectedTextureData = TextureData[selectedCharacter];

            // Sprite RendererにSpriteを適用:背景
             // Sprite RendererにSpriteを適用:背景
            if (BackGroundObject != null)
            {
                BackGroundObject.sprite = selectedTextureData.sprite;
                Debug.Log($"背景画像を'{selectedCharacter}'のImageに更新しました。");
            }
            else
            {
                Debug.LogError("BackGroundImageコンポーネントが見つかりません。");
            }

            // AnimatorにAnimator Controllerを適用
            Debug.Log("test2");
            Animator animator = BossObject.GetComponent<Animator>();
            if (animator != null)
            {
                Debug.Log("test2-1");
                animator.runtimeAnimatorController = selectedTextureData.animatorController;
                Debug.Log($"BossAnimatorを適用しました'{selectedTextureData.animatorController.name}'");
            }
            else
            {
                Debug.LogError("Player GameObjectにAnimatorコンポーネントが見つかりません。");
            }

            Debug.Log("test3");

            
            Debug.Log("test3-1");
            BGMManager.BGMtitle = selectedTextureData.BGM;
            BGMManager.updateBGMtitle(selectedTextureData.BGM);
            BGMManager.callPlayBGM();
            Debug.Log($"BGMを更新しました。 ：'{selectedCharacter}'");
    


            Debug.Log($"Textureを更新しました。 ：'{selectedCharacter}'");
        }
        else
        {
            Debug.LogWarning($"選択されたキャラクター '{selectedCharacter}' のビジュアルデータが見つかりません。");
        }
    }

    void AllBackObjectsetFalse()
    {
        holo.SetActive(false);
        niji.SetActive(false);
        vspo.SetActive(false);
    }

}
    
