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

    LootConfig config;

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

    public void DisplayLootDetails(LootConfig config) {
        ClearDetails();
        this.config = config;
        lootImage.GetComponentsInChildren<Image>()[1].sprite = config.Icon;
        lootImage.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(true);
        lootName.text = config.LootName;
        lootDescription.text = config.LootDescription;
        if (config.Type == LootType.Equipment) {
            EquipmentConfig equipment = config as EquipmentConfig;
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
        config.Use();
    }
}
