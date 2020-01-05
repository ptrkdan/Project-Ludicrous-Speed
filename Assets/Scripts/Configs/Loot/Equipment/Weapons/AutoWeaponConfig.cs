using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Equipment/AutoWeapon")]
public class AutoWeaponConfig : WeaponConfig
{
    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;

    public Projectile ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }

    public override Loot Create(bool isDefault)
    {
        return new AutoWeapon(this, isDefault);
    }

    public override Loot Create()
    {
        return new AutoWeapon(this);
    }
}
