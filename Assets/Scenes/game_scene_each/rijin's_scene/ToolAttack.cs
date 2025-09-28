using UnityEngine;

public class ToolAttack : MonoBehaviour
{
    public AttackManager attackManager;
    private void AttackDamage(Collider other)
    {
        DamageAble damageable = other.GetComponent<DamageAble>();

        if (damageable != null && attackManager.isUsingTool)
        {
            damageable.Damage(100);
        }
    }

}