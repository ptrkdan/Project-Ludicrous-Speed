using UnityEngine;

public class EquipmentConfig : LootConfig
{
    [SerializeField] private EquipmentSlot equipSlot;
    [SerializeField] private bool isEquipped;

    [Header("Stat Mod values")]
    [SerializeField] private Vector2 hullModRange;
    [SerializeField] private Vector2 shieldModRange;
    [SerializeField] private Vector2 engineModRange;
    [SerializeField] private Vector2 weaponModRange;
    [SerializeField] private Vector2 auxModRange;

    public EquipmentSlot EquipSlot { get => equipSlot; }
    public bool IsEquipped { get => isEquipped; }
    public Vector2 HullModRange { get => hullModRange; }
    public Vector2 ShieldModRange { get => shieldModRange; }
    public Vector2 EngineModRange { get => engineModRange; }
    public Vector2 WeaponModRange { get => weaponModRange; }
    public Vector2 AuxModRange { get => auxModRange; }

    public virtual Loot Create(bool isDefault)
    {
        return new Equipment(this, isDefault);
    }

    public override Loot Create()
    {
        return new Equipment(this);

    }
}
