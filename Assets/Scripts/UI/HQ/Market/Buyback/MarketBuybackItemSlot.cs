﻿using UnityEngine;
using UnityEngine.UI;

public class MarketBuybackItemSlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] MarketBuybackItemDetailsView detailsView;
    [SerializeField] Image icon;

    Loot loot;

    private void Start()
    {
        detailsView = FindObjectOfType<MarketBuybackItemDetailsView>();
    }

    public void DisplayLoot(Loot newLoot)
    {
        loot = newLoot;
        icon.sprite = loot.GetIcon();
    }

    public void ClearSlot()
    {
        Destroy(gameObject);
    }

    public void DisplayLootDetails()
    {
        detailsView.DisplayLootDetails(loot);
    }
}
