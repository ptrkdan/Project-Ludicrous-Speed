﻿using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Pick-Up Loot")]
public class PickUpLootConfig : LootConfig
{
    [SerializeField] LootController lootPrefab;
    
    [SerializeField] [Range(0,1)] float dropRate = 0.5f;
    [SerializeField] float damageValue = 0;
    [SerializeField] DamageType damageType;
    [SerializeField] float healValue = 0;
    [SerializeField] ArmourType healType;
    [SerializeField] float boostValue = 0;
    [SerializeField] StatType buffType;

    public LootController LootPrefab { get => lootPrefab; }
    public float DropRate { get => dropRate; set => dropRate = value; }
    public float DamageValue { get => damageValue; set => damageValue = value; }
    public DamageType DamageType { get => damageType; set => damageType = value; }
    public float HealValue { get => healValue; set => healValue = value; }
    public ArmourType HealType { get => healType; set => healType = value; }
    public float BoostValue { get => boostValue; set => boostValue = value; }
    public StatType BuffType { get => buffType; set => buffType = value; }
}
