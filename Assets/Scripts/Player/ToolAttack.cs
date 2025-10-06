using UnityEngine;

public class ToolAttack : MonoBehaviour
{
    public Ken_Attack ken_Attack;

    public Mito_Attack mito_Attack;

    public Sora_Attack sora_Attack;
    public Sora_Skill sora_Skill;

    public Uruha_Attack uruha_Attack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string selectedCharacter = GameData_Manager.Instance.selectedCharacter;

        DamageAble damageable = other.GetComponent<DamageAble>();

        switch (selectedCharacter)
        {
            case "剣持刀也":

                Debug.Log("衝突検知:" + other.GetComponent<DamageAble>());

                if (damageable != null)
                {
                    Debug.Log("DamageAbleコンポーネント取得");
                }
                if (damageable != null && ken_Attack.isUsingTool)
                {
                    Debug.Log("ダメージ処理の実行");
                    damageable.Damage(100);
                }

                break;

            case "月ノ美兎":

                Debug.Log("衝突検知:" + other.GetComponent<DamageAble>());

                if (damageable != null)
                {
                    Debug.Log("DamageAbleコンポーネント取得");
                }

                if (damageable != null && mito_Attack.isUsingTool)
                {
                    Debug.Log("ダメージ処理の実行");
                    damageable.Damage(100);
                }

                break;

            case "ときのそら":

                Debug.Log("衝突検知:" + other.GetComponent<DamageAble>());

                if (damageable != null)
                {
                    Debug.Log("DamageAbleコンポーネント取得");
                }

                if (damageable != null && sora_Attack.isUsingTool)
                {
                    Debug.Log("ダメージ処理の実行");
                    damageable.Damage(100);
                }
                else if (damageable != null && sora_Skill.isUsingTool)
                {
                    Debug.Log("ダメージ処理の実行");
                    damageable.Damage(200);
                }

                break;

            case "一ノ瀬うるは":

                Debug.Log("衝突検知:" + other.GetComponent<DamageAble>());

                if (damageable != null)
                {
                    Debug.Log("DamageAbleコンポーネント取得");
                }

                if (damageable != null && uruha_Attack.isUsingTool)
                {
                    Debug.Log("ダメージ処理の実行");
                    damageable.Damage(100);
                }

                break;

        }
    }

}