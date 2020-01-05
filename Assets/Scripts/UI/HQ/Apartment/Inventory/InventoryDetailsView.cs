﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDetailsView : Overlay
{

    [Header("UI References - Item Info")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;

    [Header("UI References - Item Stats")]
    [SerializeField] private RectTransform statsPanel;
    [SerializeField] private TextMeshProUGUI hullValue;
    [SerializeField] private TextMeshProUGUI shieldValue;
    [SerializeField] private TextMeshProUGUI engineValue;
    [SerializeField] private TextMeshProUGUI weaponValue;
    [SerializeField] private TextMeshProUGUI auxValue;

    [Header("UI References - Controls")]
    [SerializeField] private RectTransform controlsPanel;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button compareButton;
    [SerializeField] private Button equipButton;
    private Loot item;

    public void DisplayLootDetails(Loot item)
    {
        ClearDetails();
        this.item = item;
        itemName.text = item.GetName();
        itemCost.text = $"Value: {item.GetCreditValue()} Cr";
        itemIcon.gameObject.SetActive(true);
        itemDescription.text = item.GetDescription();
        itemIcon.GetComponentsInChildren<Image>()[1].sprite = item.GetIcon();
        controlsPanel.gameObject.SetActive(true);
        if (item.GetLootType() == LootType.Equipment)
        {
            Equipment equipment = item as Equipment;
            compareButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
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
            equipButton.gameObject.SetActive(false);
        }
    }

    public void OnSell()
    {
        InventoryManager inventory = InventoryManager.instance;
        inventory.AddToCredits(item.GetCreditValue());
        inventory.RemoveFromPlayerInventory(item);
        inventory.AddToBuybackInventory(item);
        ClearDetails();
    }

    public void OnCompare()
    {
        Debug.Log($"Comparing {item.GetName()} with currently equipped item ");
    }

    public void OnEquip()
    {
        item.Use();
        ClearDetails();
    }

    private void OnEnable()
    {
        ClearDetails();
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
