using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Generic Loot")]
// [InitializeOnLoad]
public class LootConfig : ScriptableObject
{
    [SerializeField] string lootName;
    [SerializeField] LootType lootType;
    [SerializeField] Sprite icon;
    [SerializeField] int creditValue = 0;
    [SerializeField] [TextArea (3,5)] string lootDescription;

    public string LootName { get => lootName; }
    public LootType LootType { get => lootType; }
    public string LootDescription { get => lootDescription; }
    public Sprite Icon { get => icon; }
    public int CreditValue { get => creditValue; }

    public virtual Loot Create()
    {
        return new Loot(this);
    }
}

public class Loot : ScriptableObject
{
    new string name;
    string description;
    int creditValue;
    LootType lootType;
    Sprite icon;

    public Loot() { }

    public Loot(LootConfig config)
    {
        name = config.LootName;
        description = config.LootDescription;
        creditValue = config.CreditValue;
        lootType = config.LootType;
        icon = config.Icon;
    }

    public string GetName() => name;
    public string GetDescription() => description;
    public int GetCreditValue() => creditValue;
    public LootType GetLootType() => lootType;
    public Sprite GetIcon() => icon;
    
    public virtual void Use()
    {
        Debug.Log($"Using {name}...");
    }

    public void RemoveFromInventory()
    {
        InventoryManager.instance.RemoveFromInventory(this);
    }

}

public enum LootType { Currency, Loot, Equipment }
public enum DamageType { None, Laser, Ballistic, Bio }
public enum ArmourType { None, Shield, Hull, Metal, Bio, Mineral }
public enum StatType { None, Hull, Shield, Engine, Weapon, Aux }
