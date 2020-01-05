using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Equipment/Burst Weapon")]
public class BurstWeaponConfig : WeaponConfig
{
    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField, Range(5, 45)] private float burstRadius;
    [SerializeField] private int numOfShots;

    public Projectile ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }
    public float BurstRadius { get => burstRadius; set => burstRadius = value; }
    public int NumOfShots { get => numOfShots; set => numOfShots = value; }

    public override Loot Create(bool isDefault)
    {
        return new BurstWeapon(this, isDefault);
    }

    public override Loot Create()
    {
        return new BurstWeapon(this);
    }
}
