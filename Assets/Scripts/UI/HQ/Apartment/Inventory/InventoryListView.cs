﻿using System;
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
        if (!inventoryGrid) {       // Is there a clearer way to do this?
            inventoryGrid = FindObjectOfType<InventoryGrid>();
        }
        InventorySlot[] slots = inventoryGrid.GetComponentsInChildren<InventorySlot>();
        foreach(InventorySlot slot in slots) {
            slot.ClearSlot();
        }
    }
}
