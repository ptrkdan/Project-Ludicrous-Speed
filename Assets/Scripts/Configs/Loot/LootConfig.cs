using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Generic Loot")]
public class LootConfig : ScriptableObject
{
    [SerializeField] string lootName;
    [SerializeField] Sprite lootSprite;
    [SerializeField] int lootValue = 0;
    [SerializeField] [TextArea (3,5)] string lootDescription;

    public string LootName { get => lootName; set => lootName = value; }
    public string LootDescription { get => lootDescription; set => lootDescription = value; }
    public Sprite LootSprite { get => lootSprite; set => lootSprite = value; }
    public int LootValue { get => lootValue; set => lootValue = value; }

    public virtual void Use() { }
}

public enum DamageType { None, Laser, Ballistic, Bio }
public enum ArmourType { None, Shield, Hull, Metal, Bio, Mineral }
public enum BuffType { None, Hull, Shield, Engine, Weapon, Aux }
