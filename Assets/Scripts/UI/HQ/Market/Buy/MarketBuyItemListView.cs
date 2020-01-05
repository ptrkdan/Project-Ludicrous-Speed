using System.Collections.Generic;
using UnityEngine;

public class MarketBuyItemListView : Overlay
{
    [Header("UI References")]
    [SerializeField] private ItemGrid itemGrid;

    [Header("UI Prefabs")]
    [SerializeField] private MarketBuyItemSlot itemSlotPrefab;

    private List<Loot> itemList;

    private void OnEnable()
    {
        if (itemList == null)       // TODO: the item list will only be renewed on specific times
        {
            itemList = new List<Loot>();
            StockMarket();
        }
        InventoryManager.instance.onMarketInvetoryChangedCallback += UpdateItemList;
        UpdateItemList();
    }

    private void StockMarket()
    {
        itemList = InventoryManager.instance.GetMarketInventory();
    }

    private void UpdateItemList()
    {
        ClearItemList();
        for (int i = 0; i < itemList.Count; i++)
        {
            Loot loot = itemList[i];
            MarketBuyItemSlot slot = Instantiate(itemSlotPrefab, itemGrid.transform);
            slot.DisplayLoot(loot);
        }
    }

    private void ClearItemList()
    {
        if (itemGrid == null)
        {
            itemGrid = FindObjectOfType<ItemGrid>();
        }
        MarketBuyItemSlot[] slots = itemGrid.GetComponentsInChildren<MarketBuyItemSlot>();
        foreach (MarketBuyItemSlot slot in slots)
        {
            slot.ClearSlot();
        }
    }
}
