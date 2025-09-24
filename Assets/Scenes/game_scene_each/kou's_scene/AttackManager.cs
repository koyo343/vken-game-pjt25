using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour
{

    //道具オブジェクトの情報
    public GameObject toolObject;
    public float toolRotationTime = 1.0f;
    public bool isUsingTool = false;
    private CharacterAttack currentAttackComponent;
    //スキルのクールタイム管理インスタンス取得
    //public SkillRecastManager skillManager;
    private string selectedCharacter;
    public GameData_Manager GameData_Manager;

    void Start()
    {

        //道具オブジェクトを非表示にする
        if (toolObject != null)
        {
            toolObject.SetActive(false);
        }
        //キャラの情報を取得
        if (GameData_Manager.Instance.selectedCharacter != null)
        {
            // If it exists, get the selected character
            selectedCharacter = GameData_Manager.Instance.selectedCharacter;
            Debug.Log("Selected Character: " + selectedCharacter);
        }
        else
        {
            // If the instance is null, log an error
            Debug.LogError("Error: GameData_Manager instance is not available. Make sure it's in the scene and initialized.");
        }

        switch (selectedCharacter)
        {
            case "剣持刀也":
                currentAttackComponent = GetComponent<Ken_Attack>();
                break;

            case "ときのそら":
                currentAttackComponent = GetComponent<Sora_Attack>();
                if (currentAttackComponent == null)
                {
                    Debug.LogError("Sora_Attackコンポーネントが見つかりません");
                }
                else
                {
                    Debug.Log("Sora_Attackコンポーネントの読み込みに成功しました。");
                }
                break;

            case "月ノ美兎":
                currentAttackComponent = GetComponent<Mito_Attack>();
                break;

            case "一ノ瀬うるは":
                currentAttackComponent = GetComponent<Uruha_Attack>();
                break;

            default:
                Debug.LogWarning("未対応のキャラクターです: " + selectedCharacter);
                break;
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
        /*if (Input.GetKeyDown(KeyCode.F) && skillManager.IsSkillReady)
        {
            //skillManager.UseSkill();
            StartCoroutine(SkillRoutine());
        }
        */
    }
    /*private IEnumerator SkillRoutine()
        {
        // 1. isSkillをtrueにする
        animator.SetBool("isSkill", true);

        // 2. 委員長のスキル継続時間
        yield return new WaitForSeconds(0.5f);

        // 3. 0.5秒後にisSkillをfalseにする
        animator.SetBool("isSkill", false);
        }
    */
}