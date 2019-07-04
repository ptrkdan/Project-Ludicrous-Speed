using UnityEngine;

public class Loot : MonoBehaviour
{
    protected new string name;
    protected string description;
    protected int creditValue;
    protected LootType lootType;
    protected Sprite icon;
    protected int itemDictKey;

    public Loot() { }

    public Loot(LootConfig config)
    {
        name = config.LootName;
        description = config.LootDescription;
        creditValue = config.CreditValue;
        lootType = config.LootType;
        icon = config.Icon;
        itemDictKey = config.ItemDictKey;
    }

    public string GetName() => name;
    public string GetDescription() => description;
    public int GetCreditValue() => creditValue;
    public LootType GetLootType() => lootType;
    public Sprite GetIcon() => icon;
    public int GetItemDictKey() => itemDictKey;

    public virtual void Use()
    {
        //Debug.Log($"Using {name}...");
    }

    public void RemoveFromInventory()
    {
        InventoryManager.instance.RemoveFromPlayerInventory(this);
    }

}

public enum LootType { Currency, Loot, Equipment }
public enum DamageType { None, Laser, Ballistic, Bio }
public enum ArmourType { None, Shield, Hull, Metal, Bio, Mineral }
public enum StatType { Hull, Shield, Engine, Weapon, Aux }
