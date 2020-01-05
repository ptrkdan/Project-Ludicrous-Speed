using UnityEngine;

public class InventoryListView : Overlay
{
    [Header("Cached")]
    [SerializeField] private InventoryManager inventory;

    [Header("UI References")]
    [SerializeField] private ItemGrid inventoryGrid;

    [Header("UI Prefabs")]
    [SerializeField] private ApartmentInventorySlot inventorySlotPrefab;

    private void OnEnable()
    {
        inventory = InventoryManager.instance;
        inventory.onPlayerInventoryChangedCallback += UpdateInventory;

        UpdateInventory();
    }

    private void OnDisable()
    {
        inventory.onPlayerInventoryChangedCallback -= UpdateInventory;
    }

    private void UpdateInventory()
    {
        ClearInventory();
        for (int i = 0; i < inventory.GetPlayerInventory().Count; i++)
        {
            Loot loot = inventory.GetPlayerInventory()[i];
            ApartmentInventorySlot inventorySlot = Instantiate(inventorySlotPrefab, inventoryGrid.transform);
            inventorySlot.DisplayLoot(loot);
        }
    }

    private void ClearInventory()
    {
        if (!inventoryGrid)
        {       // Is there a clearer way to do this?
            inventoryGrid = FindObjectOfType<ItemGrid>();
        }
        ApartmentInventorySlot[] slots = inventoryGrid.GetComponentsInChildren<ApartmentInventorySlot>();
        foreach (ApartmentInventorySlot slot in slots)
        {
            slot.ClearSlot();
        }
    }
}
