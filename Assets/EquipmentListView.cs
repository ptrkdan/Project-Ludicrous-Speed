using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentListView : OverlayView
{
    [Header("UI References")]
    [SerializeField] InventoryGrid equipmentGrid
        ;

    [Header("UI Prefabs")]
    [SerializeField] InventorySlot inventorySlotPrefab;

    PlayerSingleton player;

    private void Awake() {
        player = FindObjectOfType<PlayerSingleton>();
    }

    private void ClearInventoryGrid() {
        if (!equipmentGrid) {
            equipmentGrid = FindObjectOfType<InventoryGrid>();
        }
        InventorySlot[] slots = equipmentGrid.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in slots) {
            slot.ClearSlot();
        }
    }
    public void DisplayLootForEquipmentSlot(EquipmentSlot slot) {
        ClearInventoryGrid();
        List<LootConfig> equipments = player.GetInventory().FindAll(
            (loot) => loot.GetType().BaseType == typeof(EquipmentConfig)
        );
        for (int i = 0; i < equipments.Count; i++) {
            EquipmentConfig equipment = equipments[i] as EquipmentConfig;
            if (equipment.EquipSlot == slot) {
                InventorySlot inventorySlot = Instantiate(inventorySlotPrefab, equipmentGrid.transform);
                inventorySlot.DisplayLoot(equipment);
            }
        }
    }

}
