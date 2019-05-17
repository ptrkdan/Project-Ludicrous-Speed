using System.Collections.Generic;
using UnityEngine;

public class MarketBuybackItemListView : Overlay
{
    [Header("UI References")]
    [SerializeField] ItemGrid itemGrid;

    [Header("UI Prefabs")]
    [SerializeField] MarketBuybackItemSlot itemSlotPrefab;

    List<Loot> itemList;

    private void OnEnable()
    {
        if (itemList == null)       // TODO: the item list will only be renewed on specific times
        {
            itemList = new List<Loot>();
            StockMarket();
        }
        InventoryManager.instance.onBuybackInventoryChangedCallback += UpdateItemList;
        UpdateItemList();
    }

    private void OnDisable()
    {
        InventoryManager.instance.onBuybackInventoryChangedCallback -= UpdateItemList;
    }

    private void StockMarket()
    {
        itemList = InventoryManager.instance.GetBuybackInventory();
    }

    private void UpdateItemList()
    {
        ClearItemList();
        for (int i = 0; i < itemList.Count; i++)
        {
            Loot loot = itemList[i];
            MarketBuybackItemSlot slot = Instantiate(itemSlotPrefab, itemGrid.transform);
            slot.DisplayLoot(loot);
        }
    }

    private void ClearItemList()
    {
        if (itemGrid == null)
        {
            itemGrid = FindObjectOfType<ItemGrid>();
        }
        MarketBuybackItemSlot[] slots = itemGrid.GetComponentsInChildren<MarketBuybackItemSlot>();
        foreach (MarketBuybackItemSlot slot in slots)
        {
            slot.ClearSlot();
        }
    }
}
