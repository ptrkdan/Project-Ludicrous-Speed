using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryListView : Overlay
{
    [Header("Cached")]
    [SerializeField] InventoryManager inventory;

    [Header("UI References")]
    [SerializeField] InventoryGrid inventoryGrid;

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
            Loot lootConfig = inventory.Inventory[i];
            ApartmentInventorySlot inventorySlot = Instantiate(inventorySlotPrefab, inventoryGrid.transform);
            inventorySlot.DisplayLoot(lootConfig);            
        }
    }

    private void ClearInventory() {
        if (!inventoryGrid) {       // Is there a clearer way to do this?
            inventoryGrid = FindObjectOfType<InventoryGrid>();
        }
        ApartmentInventorySlot[] slots = inventoryGrid.GetComponentsInChildren<ApartmentInventorySlot>();
        foreach(ApartmentInventorySlot slot in slots) {
            slot.ClearSlot();
        }
    }
}
