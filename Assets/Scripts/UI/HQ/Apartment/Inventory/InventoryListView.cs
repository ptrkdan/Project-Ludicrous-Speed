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
        List<LootConfig> inventory = player.GetInventory();
        for (int i = 0; i < inventory.Count; i++) {
            LootConfig lootConfig = inventory[i];
            InventorySlot inventorySlot = Instantiate(inventorySlotPrefab, inventoryGrid.transform);
            inventorySlot.DisplayLoot(lootConfig);
            
        }
    }
}
