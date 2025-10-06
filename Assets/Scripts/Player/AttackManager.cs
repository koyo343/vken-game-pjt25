using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour
{

    //道具オブジェクトの情報
    public GameObject toolObject;
    public float toolRotationTime = 1.0f;
    public bool isUsingTool = false;
    private CharacterAttack currentAttackComponent;
    private CharacterSkill currentSkillComponent;
    public GameData_Manager GameData_Manager;
    public SkillRecastManager SkillRecastManager;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        GameData_Manager.CheckNullInstance();
        //道具オブジェクトを非表示にする
        if (toolObject != null)
        {
            toolObject.SetActive(false);
        }
        string selectedCharacter = GameData_Manager.Instance.selectedCharacter;

        switch (selectedCharacter)
        {
            case "剣持刀也":
                currentAttackComponent = GetComponent<Ken_Attack>();
                currentSkillComponent = GetComponent<Ken_Skill>();
                break;

            case "ときのそら":
                currentAttackComponent = GetComponent<Sora_Attack>();
                currentSkillComponent = GetComponent<Sora_Skill>();

                if (currentAttackComponent == null)
                {
                    Debug.LogError("Sora_Attackコンポーネントが見つかりません");
                }
                else
                {
                    Debug.Log("Sora_Attackコンポーネントの読み込みに成功しました");
                }
                if (currentSkillComponent == null)
                {
                    Debug.LogError("Sora_Skillコンポーネントが見つかりません");
                }
                else
                {
                    Debug.Log("Sora_Skillコンポーネントの読み込みに成功しました");
                }

                break;

            case "月ノ美兎":
                currentAttackComponent = GetComponent<Mito_Attack>();
                currentSkillComponent = GetComponent<Mito_Skill>();
                break;

            case "一ノ瀬うるは":
                currentAttackComponent = GetComponent<Uruha_Attack>();
                currentSkillComponent = GetComponent<Uruha_Skill>();
                break;

            default:
                Debug.LogWarning("未対応のキャラクターです: " + selectedCharacter);
                break;
        }

        // SkillRecastManagerとcurrentSkillComponentが正しく設定されているか確認
        if (SkillRecastManager != null && currentSkillComponent != null)
        {
            // 取得したスキルコンポーネントからSkillRecastTimeを取得し、
            // SkillRecastManagerの初期化メソッドに渡す
            SkillRecastManager.InitializeSkill(currentSkillComponent.SkillRecastTime);
        }
        else
        {
            if (SkillRecastManager == null)
            {
                Debug.LogError("SkillRecastManagerがインスペクターに設定されていません");
            }
            if (currentSkillComponent == null)
            {
                Debug.LogError("対応するスキルコンポーネントが見つかりませんでした。キャラクター名やアタッチ状況を確認してください。");
            }
        }
    }

    void Update()
    {
        //通常攻撃の呼び出し
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentAttackComponent != null)
        {
            toolObject.SetActive(true);
            currentAttackComponent.PerformAttack();
        }

        //スキルの呼び出し
        if(Input.GetKeyDown(KeyCode.F) && currentSkillComponent != null)
        {
            if(SkillRecastManager.IsSkillReady)
            {
                StartCoroutine(SkillRoutine());
                currentSkillComponent.PerformSkill();
            }
            else
            {
                Debug.Log("skill not ready あと " + SkillRecastManager.recastTime + " - " + SkillRecastManager.currentRecastTime + " 秒");
            }
        }
    }
    //スキルのアニメーション処理
    private IEnumerator SkillRoutine()
    {
        // 1. isSkillをtrueにする
        animator.SetBool("isSkill", true);

        // 2. 委員長のスキル継続時間
        yield return new WaitForSeconds(0.5f);

        // 3. 0.5秒後にisSkillをfalseにする
        animator.SetBool("isSkill", false);
    }
    public void CharaSwitch(string selectedCharacter)
    {
        switch (selectedCharacter)
        {
            case "剣持刀也":
                currentAttackComponent = GetComponent<Ken_Attack>();
                currentSkillComponent = GetComponent<Ken_Skill>();
                break;

            case "ときのそら":
                currentAttackComponent = GetComponent<Sora_Attack>();
                currentSkillComponent = GetComponent<Sora_Skill>();

                if (currentAttackComponent == null)
                {
                    Debug.LogError("Sora_Attackコンポーネントが見つかりません");
                }
                else
                {
                    Debug.Log("Sora_Attackコンポーネントの読み込みに成功しました");
                }
                if (currentSkillComponent == null)
                {
                    Debug.LogError("Sora_Skillコンポーネントが見つかりません");
                }
                else
                {
                    Debug.Log("Sora_Skillコンポーネントの読み込みに成功しました");
                }

                break;

            case "月ノ美兎":
                currentAttackComponent = GetComponent<Mito_Attack>();
                currentSkillComponent = GetComponent<Mito_Skill>();
                break;

            case "一ノ瀬うるは":
                currentAttackComponent = GetComponent<Uruha_Attack>();
                currentSkillComponent = GetComponent<Uruha_Skill>();
                break;

            default:
                Debug.LogWarning("未対応のキャラクターです: " + selectedCharacter);
                break;
        }
    }

}