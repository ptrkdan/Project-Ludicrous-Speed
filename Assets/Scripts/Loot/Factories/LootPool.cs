using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Loot Pool")]
public class LootPool : ScriptableObject
{
    [SerializeField] private LootRarity rarity;
    [SerializeField] [Range(0f, 1f)] private float dropRate;
    [SerializeField] private List<LootConfig> lootList;

    public LootRarity GetRarity() => rarity;
    public List<LootConfig> GetLootList() => lootList;

    public LootConfig RollForLoot()
    {
        LootConfig droppedLoot = null;

        float roll = Random.Range(0f, 1f);
        if (roll >= 1f - dropRate)
        {
            droppedLoot = SelectLoot();
        }

        return droppedLoot;
    }

    private LootConfig SelectLoot()
    {
        return lootList[Random.Range(0, lootList.Count)];
    }
}
