using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Equipment/Weapon/Weapon Factory")]
public class WeaponFactory : EquipmentFactory
{
    public override LootConfig DropLoot(LootRarity minRarity, LootRarity maxRarity)
    {
        WeaponConfig droppedLoot = null;
        LootRarity poolRarity = maxRarity;      // Prioritize higher rarity drops

        while (poolRarity >= minRarity && poolRarity >= 0)
        {
            droppedLoot = lootPools[(int)poolRarity]?.RollForLoot() as WeaponConfig;
            if (droppedLoot)
            {
                return droppedLoot;     // return as soon as a loot has been selected
            }

            poolRarity--;
        }

        return droppedLoot;             // If no loot drops, returns null
    }
}
