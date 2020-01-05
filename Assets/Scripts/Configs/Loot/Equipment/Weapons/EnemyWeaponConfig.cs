using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Weapon Config")]
public class EnemyWeaponConfig : AutoWeaponConfig
{
    [Header("Misc.")]
    [SerializeField] private float cooldownVariation;

    public float CooldownVariation { get => cooldownVariation; set => cooldownVariation = value; }

    public override Loot Create()
    {
        return new EnemyWeapon(this);
    }
}
