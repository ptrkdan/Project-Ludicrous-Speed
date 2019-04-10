using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Weapon Config")]
public class EnemyWeaponConfig : WeaponConfig 
{
    [Header("Misc.")]
    [SerializeField] float cooldownVariation;

    public float CooldownVariation { get => cooldownVariation; set => cooldownVariation = value; }
}
