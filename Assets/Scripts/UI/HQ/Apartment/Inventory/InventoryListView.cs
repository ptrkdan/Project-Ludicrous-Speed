using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryListView : OverlayView
{
    [Header("Cached")]
    [SerializeField] InventoryManager inventory;

    [Header("UI References")]
    [SerializeField] InventoryGrid inventoryGrid;

    [Header("UI Prefabs")]
    [SerializeField] InventorySlot inventorySlotPrefab;

    private void Awake() {
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateInventory;

        UpdateInventory();
    }

    private void UpdateInventory() {
        ClearInventory();
        for (int i = 0; i < inventory.Inventory.Count; i++) {
            LootConfig lootConfig = inventory.Inventory[i];
            InventorySlot inventorySlot = Instantiate(inventorySlotPrefab, inventoryGrid.transform);
            inventorySlot.DisplayLoot(lootConfig);            
        }
    }

    private void ClearInventory() {
        InventorySlot[] slots = inventoryGrid.GetComponentsInChildren<InventorySlot>();
        foreach(InventorySlot slot in slots) {
            slot.ClearSlot();
        }
    }
}
