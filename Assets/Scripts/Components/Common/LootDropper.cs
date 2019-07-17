using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] bool specialLootOverride;
    [SerializeField] PickUpLootConfig specialLootConfig;

    private void OnDestroy()
    {
        DropLoot();
    }

    private void DropLoot()
    {
        PickUpLootConfig lootConfig = null;
        if (specialLootOverride)
        {
            lootConfig = specialLootConfig;
        }
        else
        {
            lootConfig = LootManager.instance.DropLoot();
            
        }

        if (lootConfig != null)
        {
            LootController loot = Instantiate(lootConfig.LootPrefab, LootManager.instance.transform);
            loot.Drop(lootConfig, transform.position);
        }
    }
}
