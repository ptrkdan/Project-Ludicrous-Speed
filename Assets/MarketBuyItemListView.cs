using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketBuyItemListView : Overlay
{
    [Header("UI References")]
    [SerializeField] ItemGrid itemGrid;

    [Header("UI Prefabs")]
    [SerializeField] MarketBuyItemSlot itemSlotPrefab;

    [Header("Item list properties")]
    [SerializeField] int numItems = 9;

    [Header("[DEBUG]")]
    [SerializeField] List<LootConfig> availableItems;


    List<Loot> itemList;

    private void OnEnable()
    {
        if (itemList == null)       // TODO: the item list will only be renewed on specific times
        {
            itemList = new List<Loot>();
            PopulateItemList();
        }
        UpdateItemList();
    }

    private void PopulateItemList()
    {
        if (availableItems.Count == 0) return;
        for (int i = 0; i < numItems; i++)
        {
            Loot item = availableItems[Random.Range(0, availableItems.Count)].Create();
            itemList.Add(item);
        }
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
        MarketBuyItemSlot[] slots = itemGrid.GetComponentsInChildren<MarketBuyItemSlot>();
    }
}
