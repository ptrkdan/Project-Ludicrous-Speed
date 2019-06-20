using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class LootFactory : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] protected LootPool[] lootPools;

    public Sprite Icon { get => icon; }

    private void Awake()
    {
        lootPools = new LootPool[Enum.GetValues(typeof(LootRarity)).Length];
    }

    public abstract LootConfig DropLoot(LootRarity minRarity, LootRarity maxRarity);
}
