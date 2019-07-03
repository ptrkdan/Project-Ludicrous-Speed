using UnityEngine;


public class Equipment : Loot
{
    bool isEquipped;
    bool isDefault;
    EquipmentSlot equipSlot;
    float hullMod;
    float shieldMod;
    float engineMod;
    float weaponMod;
    float auxMod;

    public bool IsEquipped { get; set; }
    public bool IsDefault { get; set; }
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

    public void SetStatModValueFromSave(float[] stats)
    {
        hullMod = stats[(int)StatType.Hull];
        shieldMod = stats[(int)StatType.Shield];
        engineMod = stats[(int)StatType.Engine];
        weaponMod = stats[(int)StatType.Weapon];
        auxMod = stats[(int)StatType.Aux];
    }

    public Equipment() : base() { }

    public Equipment(EquipmentConfig config, bool isDefault = false) : base(config)
    {
        this.isDefault = isDefault;
        isEquipped = false;
        equipSlot = config.EquipSlot;
        hullMod = Mathf.Floor(Random.Range(config.HullModRange.x, config.HullModRange.y));
        shieldMod = Mathf.Floor(Random.Range(config.ShieldModRange.x, config.ShieldModRange.y));
        engineMod = Mathf.Floor(Random.Range(config.EngineModRange.x, config.EngineModRange.y));
        weaponMod = Mathf.Floor(Random.Range(config.WeaponModRange.x, config.WeaponModRange.y));
        auxMod = Mathf.Floor(Random.Range(config.AuxModRange.x, config.AuxModRange.y));
    }

    public Equipment(int[] stats, EquipmentConfig config, bool isEquipped, bool isDefault = false) : base(config)
    {
        this.isDefault = isDefault;
        this.isEquipped = isEquipped;
        equipSlot = config.EquipSlot;
        hullMod = stats[(int)StatType.Hull];
        shieldMod = stats[(int)StatType.Shield];
        engineMod = stats[(int)StatType.Engine];
        weaponMod = stats[(int)StatType.Weapon];
        auxMod = stats[(int)StatType.Aux];
    }

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    public virtual void Activate() { }

    public virtual void Deactivate() { }
}

public enum EquipmentSlot { PrimaryWeapon, SecondaryWeapon, Support, PrimaryMod, SecondaryMod }
