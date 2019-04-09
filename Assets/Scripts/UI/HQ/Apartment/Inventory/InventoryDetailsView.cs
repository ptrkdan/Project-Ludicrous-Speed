using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDetailsView : OverlayView 
{

    [Header("UI References")]
    [SerializeField] Image lootImage;
    [SerializeField] TextMeshProUGUI lootName;
    [SerializeField] TextMeshProUGUI lootDescription;
    [SerializeField] Button equipButton;

    LootConfig config;

    private void Start() {
        ClearDetails();
    }

    private void ClearDetails() {
        lootImage.gameObject.SetActive(false);
        lootName.text = "";
        lootDescription.text = "";
        equipButton.gameObject.SetActive(false);
    }

    public void DisplayLootDetails(LootConfig config) {
        ClearDetails();
        this.config = config;
        lootImage.sprite = config.Icon;
        lootImage.gameObject.SetActive(true);
        lootName.text = config.LootName;
        lootDescription.text = config.LootDescription;
        if (config.Type == LootType.Equipment) {
            equipButton.gameObject.SetActive(true);
        }
    }

    public void OnEquip() {
        config.Use();
    }
}
