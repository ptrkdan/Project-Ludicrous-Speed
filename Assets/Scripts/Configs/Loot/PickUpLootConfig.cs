using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Pick-Up Loot")]
public class PickUpLootConfig : LootConfig
{
    [SerializeField] LootController lootPrefab;
    
    [Header("Stats")]
    [SerializeField] [Range(0,1)] float spawnRate = 0.5f;
    [SerializeField] float damageValue = 0;
    [SerializeField] DamageType damageType;
    [SerializeField] float healValue = 0;
    [SerializeField] ArmourType healType;

    public LootController LootPrefab { get => lootPrefab; }
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
    public float DamageValue { get => damageValue; set => damageValue = value; }
    public DamageType DamageType { get => damageType; set => damageType = value; }
    public float HealValue { get => healValue; set => healValue = value; }
    public ArmourType HealType { get => healType; set => healType = value; }
}
