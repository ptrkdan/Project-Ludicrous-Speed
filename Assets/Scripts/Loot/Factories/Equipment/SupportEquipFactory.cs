using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Equipment/Weapon/Support Equipment Factory")]
public class SupportEquipFactory : EquipmentFactory
{
    public override LootConfig DropLoot(LootRarity minRarity, LootRarity maxRarity)
    {
        SupportEquipConfig droppedLoot = null;
        LootRarity poolRarity = maxRarity;      // Prioritize higher rarity drops

        while (poolRarity > minRarity && poolRarity >= 0)
        {
            droppedLoot = lootPools[(int)poolRarity].RollForLoot() as SupportEquipConfig;
            if (droppedLoot)
            {
                return droppedLoot;     // return as soon as a loot has been selected
            }

            poolRarity--;
        }

        return droppedLoot;             // If no loot drops, returns null
    }
}
