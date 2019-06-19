using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Item/Smugglers Item Factory")]
public class SmugglersItemFactory : ItemFactory
{
    public override LootConfig DropLoot(LootRarity minRarity, LootRarity maxRarity)
    {
        return base.DropLoot(minRarity, maxRarity);
    }
}
