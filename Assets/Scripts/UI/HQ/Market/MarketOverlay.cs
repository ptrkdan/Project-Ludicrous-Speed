using System;
using UnityEngine;
using TMPro;

public class MarketOverlay : Overlay
{
    [Header("UI Reference")]
    [SerializeField] RectTransform contentArea;
    [SerializeField] TextMeshProUGUI credits;

    [Header("UI Prefabs")]
    [SerializeField] MarketBuyItemListView buyItemListView;
    [SerializeField] MarketBuyItemDetailsView buyItemDetailsView;

    MarketTab activeTab;

    public MarketTab GetActiveTab() => activeTab;

    public override void Display() {
        base.Display();
    }

    public void ChangeTab(int nextTabIndex)
    {
        ClearContentArea();
        MarketTab nextTab = (MarketTab)nextTabIndex;
        switch (nextTab)
        {
            case MarketTab.Buy:
                MarketBuyItemListView buyItemList = Instantiate(buyItemListView, contentArea);
                MarketBuyItemDetailsView buyItemDetails = Instantiate(buyItemDetailsView, contentArea);
                break;
            case MarketTab.Sell:
            case MarketTab.Buyback:
            default:
                break;
        }
        activeTab = nextTab;
    }

    private void UpdateCredits()
    {
        credits.text = $"Credits: {InventoryManager.instance.Credits}";
    }

    private void ClearContentArea()
    {
        foreach (Overlay view in contentArea.GetComponentsInChildren<Overlay>())
        {
            Destroy(view.gameObject);
        }
    }

    private void OnEnable()
    {
        InventoryManager.instance.onItemChangedCallback += UpdateCredits;
        UpdateCredits();
        ChangeTab((int)MarketTab.Buy);
    }
}

public enum MarketTab { Buy, Sell, Buyback }
