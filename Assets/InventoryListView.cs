using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryListView : OverlayView
{
    [Header("Cached")]
    [SerializeField] PlayerSingleton player;

    [Header("UI References")]
    [SerializeField] InventoryGrid inventoryGrid;

    [Header("UI Prefabs")]
    [SerializeField] InventorySlot inventorySlotPrefab;

    private void Start() {
        player = FindObjectOfType<PlayerSingleton>();
        DisplayInventory();
    }

    private void DisplayInventory() {
        for (int i = 0; i < player.Inventory.Count - 1; i++) {
            LootConfig lootConfig = player.Inventory[i];
            InventorySlot inventorySlot = Instantiate(inventorySlotPrefab, inventoryGrid.transform);
            inventorySlot.DisplayLoot(lootConfig);
            
        }
    }
}
