using System.Collections.Generic;
using UnityEngine;

public class EquipmentConfig : LootConfig
{
    [SerializeField] EquipmentSlot equipSlot;
    [SerializeField] bool isEquipped;

    [Header("Stat Mod values")]
    [SerializeField] Vector2 hullModRange;
    [SerializeField] Vector2 shieldModRange;
    [SerializeField] Vector2 engineModRange;
    [SerializeField] Vector2 weaponModRange;
    [SerializeField] Vector2 auxModRange;

    public EquipmentSlot EquipSlot { get => equipSlot; }
    public bool IsEquipped { get => isEquipped; }
    public Vector2 HullModRange { get => hullModRange; }
    public Vector2 ShieldModRange { get => shieldModRange; }
    public Vector2 EngineModRange { get => engineModRange; }
    public Vector2 WeaponModRange { get => weaponModRange; }
    public Vector2 AuxModRange { get => auxModRange; }

    public override Loot Create()
    {
        return new Equipment(this);
        
    }
}

public class Equipment : Loot
{
    bool isEquipped;
    EquipmentSlot equipSlot;
    float hullMod;
    float shieldMod;
    float engineMod;
    float weaponMod;
    float auxMod;

    public bool IsEquipped() => isEquipped;
    public EquipmentSlot GetEquipSlot() => equipSlot;
    public float GetStatModValue(StatType type)
    {
        switch (type)
        {
            case StatType.Hull:
                return hullMod;
            case StatType.Shield:
                return shieldMod;
            case StatType.Engine:
                return engineMod;
            case StatType.Weapon:
                return weaponMod;
            case StatType.Aux:
                return auxMod;
            default:
                return 0;
        }
    }

    public Equipment() : base() { }

    public Equipment(EquipmentConfig config) : base(config)
    { 
        isEquipped = false;
        equipSlot = config.EquipSlot;
        hullMod = Mathf.Floor(Random.Range(config.HullModRange.x, config.HullModRange.y));
        shieldMod = Mathf.Floor(Random.Range(config.ShieldModRange.x, config.ShieldModRange.y));
        engineMod = Mathf.Floor(Random.Range(config.EngineModRange.x, config.EngineModRange.y));
        weaponMod = Mathf.Floor(Random.Range(config.WeaponModRange.x, config.WeaponModRange.y));
        auxMod = Mathf.Floor(Random.Range(config.AuxModRange.x, config.AuxModRange.y));
    }

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    public virtual void Fire(Vector3 projectileOrigin, Quaternion rotation) { }
}

public enum EquipmentSlot { PrimaryWeapon, SecondaryWeapon, Support, PrimaryMod, SecondaryMod}
