using TMPro;
using UnityEngine;

public class MarketOverlay : Overlay
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform contentArea;
    [SerializeField] private TextMeshProUGUI credits;

    [Header("UI Prefabs - Buy")]
    [SerializeField] private MarketBuyItemListView buyItemListView;
    [SerializeField] private MarketBuyItemDetailsView buyItemDetailsView;
    [Header("UI Prefabs - Sell")]
    [SerializeField] private MarketSellItemListView sellItemListView;
    [SerializeField] private MarketSellItemDetailsView sellItemDetailsView;
    [Header("UI Prefabs - Buyback")]
    [SerializeField] private MarketBuybackItemListView buybackItemListView;
    [SerializeField] private MarketBuybackItemDetailsView buybackItemDetailsView;

    private MarketTab activeTab;

    public MarketTab GetActiveTab() => activeTab;

    public override void Display()
    {
        base.Display();
    }

    public void ChangeTab(int nextTabIndex)
    {
        ClearContentArea();
        MarketTab nextTab = (MarketTab)nextTabIndex;
        switch (nextTab)
        {
            case MarketTab.Buy:
                Instantiate(buyItemListView, contentArea);
                Instantiate(buyItemDetailsView, contentArea);
                break;
            case MarketTab.Sell:
                Instantiate(sellItemListView, contentArea);
                Instantiate(sellItemDetailsView, contentArea);
                break;
            case MarketTab.Buyback:
                Instantiate(buybackItemListView, contentArea);
                Instantiate(buybackItemDetailsView, contentArea);
                break;
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
        InventoryManager.instance.onPlayerInventoryChangedCallback += UpdateCredits;
        UpdateCredits();
        ChangeTab((int)MarketTab.Buy);
    }

    private void OnDisable()
    {
        InventoryManager.instance.onPlayerInventoryChangedCallback -= UpdateCredits;
    }
}

public enum MarketTab { Buy, Sell, Buyback }
