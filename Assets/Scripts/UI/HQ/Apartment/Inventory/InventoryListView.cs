using UnityEngine;

public class InventoryListView : Overlay
{
    [Header("Cached")]
    [SerializeField] InventoryManager inventory;

    [Header("UI References")]
    [SerializeField] ItemGrid inventoryGrid;

    [Header("UI Prefabs")]
    [SerializeField] ApartmentInventorySlot inventorySlotPrefab;

    private void OnEnable () {
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateInventory;     

        UpdateInventory();
    }

    private void OnDisable()
    {
        inventory.onItemChangedCallback -= UpdateInventory;
    }

    private void UpdateInventory() {
        ClearInventory(); 
        for (int i = 0; i < inventory.Inventory.Count; i++) {
            Loot loot = inventory.Inventory[i];
            ApartmentInventorySlot inventorySlot = Instantiate(inventorySlotPrefab, inventoryGrid.transform);
            inventorySlot.DisplayLoot(loot);            
        }
    }

    private void ClearInventory() {
        if (!inventoryGrid) {       // Is there a clearer way to do this?
            inventoryGrid = FindObjectOfType<ItemGrid>();
        }
        ApartmentInventorySlot[] slots = inventoryGrid.GetComponentsInChildren<ApartmentInventorySlot>();
        foreach(ApartmentInventorySlot slot in slots) {
            slot.ClearSlot();
        }
    }
}
