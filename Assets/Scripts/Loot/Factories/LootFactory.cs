using System;
using UnityEngine;

public abstract class LootFactory : ScriptableObject
{
    [SerializeField] protected LootPool[] lootPools;

    private void Awake()
    {
        lootPools = new LootPool[Enum.GetValues(typeof(LootRarity)).Length];
    }

    public abstract LootConfig DropLoot(LootRarity minRarity, LootRarity maxRarity);
}
