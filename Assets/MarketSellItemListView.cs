using System;
using UnityEngine;

public class MarketSellItemListView : Overlay
{
    [Header("UI References")]
    [SerializeField] ItemGrid itemGrid;

    [Header("UI Prefab")]
    [SerializeField] MarketSellItemSlot itemSlotPrefab;

    InventoryManager inventory;

    private void OnEnable()
    {
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateInventory;

        UpdateInventory();
    }

    private void OnDisable()
    {
        inventory.onItemChangedCallback -= UpdateInventory;
    }

    private void UpdateInventory()
    {
        ClearInventory();
        for (int i = 0; i < inventory.Inventory.Count; i++)
        {
            Loot loot = inventory.Inventory[i];
            MarketSellItemSlot itemSlot = Instantiate(itemSlotPrefab, itemGrid.transform);
            itemSlot.DisplayLoot(loot);
        }
    }

    private void ClearInventory()
    {
        MarketSellItemSlot[] slots = itemGrid.GetComponentsInChildren<MarketSellItemSlot>();
        foreach(MarketSellItemSlot slot in slots)
        {
            slot.ClearSlot();
        }
    }
}
