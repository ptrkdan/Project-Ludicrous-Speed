using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Equipment/Generic Equipment Factory")]
public class EquipmentFactory : LootFactory
{
    public override LootConfig DropLoot(LootRarity minRarity, LootRarity maxRarity)
    {
        EquipmentConfig droppedLoot = null;
        LootRarity poolRarity = maxRarity;      // Prioritize higher rarity drops

        while (poolRarity >= minRarity && poolRarity >= 0)
        {
            droppedLoot = lootPools[(int)poolRarity]?.RollForLoot() as EquipmentConfig;
            if (droppedLoot)
            {
                return droppedLoot;     // return as soon as a loot has been selected
            }

            poolRarity--;
        }

        return droppedLoot;             // If no loot drops, returns null
    }
}
