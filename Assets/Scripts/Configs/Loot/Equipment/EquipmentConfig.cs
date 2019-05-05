using System.Collections.Generic;
using UnityEngine;

public class EquipmentConfig : LootConfig
{
    [SerializeField] EquipmentSlot equipSlot;
    [SerializeField] bool isEquipped;

    [Header("Stat Mod values")]
    [SerializeField] int hullMod;
    [SerializeField] int shieldMod;
    [SerializeField] int engineMod;
    [SerializeField] int weaponMod;
    [SerializeField] int auxMod;

    public EquipmentSlot EquipSlot { get => equipSlot; }
    public bool IsEquipped { get => isEquipped; }
    
    public int GetStatModValue(StatType type) {
        switch (type) { 
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
    
    public override void Use() {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    public virtual void Fire(Vector3 projectileOrigin, Quaternion rotation) { }
}

public enum EquipmentSlot { PrimaryWeapon, SecondaryWeapon, Support, PrimaryMod, SecondaryMod}
