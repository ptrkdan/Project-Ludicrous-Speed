using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentListView : Overlay
{
    [Header("UI References")]
    [SerializeField] ItemGrid equipmentGrid;

    [Header("UI Prefabs")]
    [SerializeField] HangarInventorySlot inventorySlotPrefab;

    PlayerSingleton player;
    EquipmentSlot slot;

    private void OnEnable() {
        player = FindObjectOfType<PlayerSingleton>();
        InventoryManager.instance.onItemChangedCallback += UpdateEquipmentList;
    }

    private void OnDisable()
    {
        InventoryManager.instance.onItemChangedCallback -= UpdateEquipmentList;
    }

    private void UpdateEquipmentList()
    {
        DisplayLootForEquipmentSlot(slot);
    }

    private void ClearInventoryGrid() {
        if (!equipmentGrid) {
            equipmentGrid = FindObjectOfType<ItemGrid>();
        }
        HangarInventorySlot[] slots = equipmentGrid.GetComponentsInChildren<HangarInventorySlot>();
        foreach (HangarInventorySlot slot in slots) {
            slot.ClearSlot();
        }
    }
    public void DisplayLootForEquipmentSlot(EquipmentSlot slot) {
        this.slot = slot;

        ClearInventoryGrid();
        // TODO: Add current equipment at the beginning of list

        List<Loot> equipments = player.GetInventory().FindAll(
            (loot) => loot.GetType().BaseType == typeof(Equipment)
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
