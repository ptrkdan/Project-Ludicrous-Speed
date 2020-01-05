using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] private bool specialLootOverride;
    [SerializeField] private PickUpLootConfig specialLootConfig;

    #region Methods: Unity

    private void OnDestroy()
    {
        DropLoot();
    }

    #endregion Methods: Unity

    private void DropLoot()
    {
        PickUpLootConfig lootConfig = AttemptLootDrop();
        if (lootConfig != null)
        {
            LootController loot = Instantiate(lootConfig.LootPrefab, LootManager.instance.transform);
            loot.Drop(lootConfig, transform.position);
        }
    }

    private PickUpLootConfig AttemptLootDrop()
    {
        PickUpLootConfig lootConfig;
        if (specialLootOverride)
        {
            lootConfig = specialLootConfig;
        }
        else
        {
            lootConfig = LootManager.instance.DropLoot();
        }

        return lootConfig;
    }
}
