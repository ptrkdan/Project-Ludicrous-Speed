using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Weapon Config")]
public class WeaponConfig : LootConfig
{
    [SerializeField] EquipmentSlot equipmentSlot;

    [Header("Stats")]
    [SerializeField] Stat damage;
    [SerializeField] Stat speed;
    [SerializeField] Stat cooldown;

    [Header("Projectile")]
    [SerializeField] Projectile projectile;
    [SerializeField] Vector3 projectileOffset;

    [Header("Audio")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0;
}

public enum EquipmentSlot { PrimaryWeapon, SecondaryWeapon, Support }