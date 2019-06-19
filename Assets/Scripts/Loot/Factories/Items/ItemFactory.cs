using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Item/Generic Item Factory")]
public class ItemFactory : LootFactory
{
    public override LootConfig DropLoot(LootRarity minRarity, LootRarity maxRarity)
    {
        LootConfig droppedLoot = null;
        LootRarity poolRarity = maxRarity;      // Prioritize higher rarity drops

        while (poolRarity >= minRarity && poolRarity >= 0)
        {
            droppedLoot = lootPools[(int)poolRarity]?.RollForLoot();
            if (droppedLoot)
            {
                return droppedLoot;     // return as soon as a loot has been selected
            }

            poolRarity--;
        }

        return droppedLoot;             // If no loot drops, returns null
    }
}
