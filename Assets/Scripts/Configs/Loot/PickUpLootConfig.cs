using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Pick-Up Loot")]
public class PickUpLootConfig : LootConfig
{
    [SerializeField] private LootController lootPrefab;

    [SerializeField, Range(0, 1)] private float dropRate = 0.5f;
    [SerializeField] private float damageValue = 0;
    [SerializeField] private DamageType damageType;
    [SerializeField] private float healValue = 0;
    [SerializeField] private ArmourType healType;
    [SerializeField] private float boostValue = 0;
    [SerializeField] private StatType buffType;

    public LootController LootPrefab { get => lootPrefab; }
    public float DropRate { get => dropRate; set => dropRate = value; }
    public float DamageValue { get => damageValue; set => damageValue = value; }
    public DamageType DamageType { get => damageType; set => damageType = value; }
    public float HealValue { get => healValue; set => healValue = value; }
    public ArmourType HealType { get => healType; set => healType = value; }
    public float BoostValue { get => boostValue; set => boostValue = value; }
    public StatType BuffType { get => buffType; set => buffType = value; }
}
