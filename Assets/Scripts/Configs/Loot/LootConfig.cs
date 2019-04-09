using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Generic Loot")]
public class LootConfig : ScriptableObject
{
    [SerializeField] string lootName;
    [SerializeField] LootType type;
    [SerializeField] Sprite icon;
    [SerializeField] int lootValue = 0;
    [SerializeField] [TextArea (3,5)] string lootDescription;

    public string LootName { get => lootName; set => lootName = value; }
    public LootType Type { get => type; }
    public string LootDescription { get => lootDescription; set => lootDescription = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public int LootValue { get => lootValue; set => lootValue = value; }

    public virtual void Use() {
        Debug.Log($"Using {lootName}...");

    }
}

public enum LootType { Currency, Loot, Equipment }
public enum DamageType { None, Laser, Ballistic, Bio }
public enum ArmourType { None, Shield, Hull, Metal, Bio, Mineral }
public enum BuffType { None, Hull, Shield, Engine, Weapon, Aux }
