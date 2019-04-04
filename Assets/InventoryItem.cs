using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] LootConfig config;

    [Header("UI References")]
    [SerializeField] InventoryDetailsView inventoryDetailsView;

    private void Start() {
        inventoryDetailsView = FindObjectOfType<InventoryDetailsView>();
    }

    public void DisplayItemDescription() {
        // TODO: Set details in inventoryDetailsView
    }
}
