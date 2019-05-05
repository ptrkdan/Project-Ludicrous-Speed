using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentListView : Overlay
{
    [Header("UI References")]
    [SerializeField] InventoryGrid equipmentGrid
        ;

    [Header("UI Prefabs")]
    [SerializeField] HangarInventorySlot inventorySlotPrefab;

    PlayerSingleton player;

    private void Awake() {
        player = FindObjectOfType<PlayerSingleton>();
    }

    private void ClearInventoryGrid() {
        if (!equipmentGrid) {
            equipmentGrid = FindObjectOfType<InventoryGrid>();
        }
        HangarInventorySlot[] slots = equipmentGrid.GetComponentsInChildren<HangarInventorySlot>();
        foreach (HangarInventorySlot slot in slots) {
            slot.ClearSlot();
        }
    }
    public void DisplayLootForEquipmentSlot(EquipmentSlot slot) {
        ClearInventoryGrid();
        List<Loot> equipments = player.GetInventory().FindAll(
            (loot) => loot.GetType().BaseType == typeof(EquipmentConfig)
        );
        for (int i = 0; i < equipments.Count; i++) {
            Equipment equipment = equipments[i] as Equipment;
            if (equipment.GetEquipSlot() == slot) {
                HangarInventorySlot inventorySlot = Instantiate(inventorySlotPrefab, equipmentGrid.transform);
                inventorySlot.DisplayLoot(equipment);
            }
        }
    }
}
