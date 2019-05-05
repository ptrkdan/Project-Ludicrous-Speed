using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDetailsView : Overlay 
{

    [Header("UI References")]
    [SerializeField] Image lootImage;
    [SerializeField] Button equipButton;
    [SerializeField] Button sellButton;
    [SerializeField] TextMeshProUGUI lootName;
    [SerializeField] TextMeshProUGUI lootDescription;
    [SerializeField] Transform statsPanel;
    [SerializeField] TextMeshProUGUI hullValue;
    [SerializeField] TextMeshProUGUI shieldValue;
    [SerializeField] TextMeshProUGUI engineValue;
    [SerializeField] TextMeshProUGUI weaponValue;
    [SerializeField] TextMeshProUGUI auxValue;

    Loot loot;

    private void Start() {
        ClearDetails();
    }

    private void ClearDetails() {
        lootImage.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
        lootName.text = "";
        lootDescription.text = "";
        statsPanel.gameObject.SetActive(false);
    }

    public void DisplayLootDetails(Loot loot) {
        ClearDetails();
        this.loot = loot;
        lootImage.GetComponentsInChildren<Image>()[1].sprite = loot.GetIcon();
        lootImage.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(true);
        lootName.text = loot.GetName();
        lootDescription.text = loot.GetDescription();
        if (loot.GetLootType() == LootType.Equipment) {
            Equipment equipment = loot as Equipment;
            equipButton.gameObject.SetActive(true);
            statsPanel.gameObject.SetActive(true);
            hullValue.text = equipment.GetStatModValue(StatType.Hull).ToString();
            shieldValue.text = equipment.GetStatModValue(StatType.Shield).ToString();
            engineValue.text = equipment.GetStatModValue(StatType.Engine).ToString();
            weaponValue.text = equipment.GetStatModValue(StatType.Weapon).ToString();
            auxValue.text = equipment.GetStatModValue(StatType.Aux).ToString();            
        }        
    }

    public void OnEquip() {
        loot.Use();
    }
}
