using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketSellItemDetailsView : Overlay
{
    [Header("UI References - Item Info")]
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemCost;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] Image itemIcon;

    [Header("UI References - Item Stats")]
    [SerializeField] RectTransform statsPanel;
    [SerializeField] TextMeshProUGUI hullValue;
    [SerializeField] TextMeshProUGUI shieldValue;
    [SerializeField] TextMeshProUGUI engineValue;
    [SerializeField] TextMeshProUGUI weaponValue;
    [SerializeField] TextMeshProUGUI auxValue;

    [Header("UI References - Controls")]
    [SerializeField] RectTransform controlsPanel;
    [SerializeField] Button sellButton;
    [SerializeField] Button compareButton;

    Loot item;

    private void Start()
    {
        ClearDetails();
    }

    public void DisplayLootDetails(Loot item)
    {
        ClearDetails();
        this.item = item;
        itemName.text = item.GetName();
        itemCost.text = $"{item.GetCreditValue()} Cr";
        itemDescription.text = item.GetDescription();
        itemIcon.gameObject.SetActive(true);
        itemIcon.GetComponentsInChildren<Image>()[1].sprite = item.GetIcon();
        controlsPanel.gameObject.SetActive(true);
        if (item.GetLootType() == LootType.Equipment)
        {
            Equipment equipment = item as Equipment;
            compareButton.gameObject.SetActive(true);
            statsPanel.gameObject.SetActive(true);
            hullValue.text = equipment.GetStatModValue(StatType.Hull).ToString();
            shieldValue.text = equipment.GetStatModValue(StatType.Shield).ToString();
            engineValue.text = equipment.GetStatModValue(StatType.Engine).ToString();
            weaponValue.text = equipment.GetStatModValue(StatType.Weapon).ToString();
            auxValue.text = equipment.GetStatModValue(StatType.Aux).ToString();
        }
        else
        {
            compareButton.gameObject.SetActive(false);
        }
    }

    public void OnSell()
    {
        InventoryManager inventory = InventoryManager.instance;
        inventory.AddToCredits(item.GetCreditValue());
        inventory.RemoveFromPlayerInventory(item);
        inventory.AddToBuybackInventory(item);
        ClearDetails();

        Debug.Log($"{item.GetName()} sold for ${item.GetCreditValue()}");
    }

    public void OnCompare()
    {
        Debug.Log($"Comparing {item.GetName()} with currently equipped item ");
    }

    private void ClearDetails()
    {
        itemName.text = "";
        itemCost.text = "";
        itemDescription.text = "";
        itemIcon.gameObject.SetActive(false);
        statsPanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(false);
    }
}
