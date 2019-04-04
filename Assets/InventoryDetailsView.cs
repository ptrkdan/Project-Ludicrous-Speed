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

    private void Start() {
        ClearDetails();
    }

    private void ClearDetails() {
        lootImage.gameObject.SetActive(false);
        lootName.text = "";
        lootDescription.text = "";
    }

    public void DisplayLootDetails(LootConfig config) {
        lootImage.sprite = config.LootSprite;
        lootImage.gameObject.SetActive(true);
        lootName.text = config.LootName;
        lootDescription.text = config.LootDescription;
    }
}
