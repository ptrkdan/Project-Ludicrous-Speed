using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Transform contentArea;

    [Header("UI Prefabs")]
    [SerializeField] HangarShipView shipView;
    [SerializeField] EquipmentListView equipmentListView;
    [SerializeField] EquipmentDetailsView equipmentDetailsView;

    PlayerSingleton player;

    private void OnEnable() {
        player = FindObjectOfType<PlayerSingleton>();
        DisplayShipView();
    }


    private void ClearContentArea() {
        foreach (OverlayView view in contentArea.GetComponentsInChildren<OverlayView>()) {
            Destroy(view.gameObject);
        }
    }

    private void DisplayShipView() {
        ClearContentArea();
        HangarShipView ship = Instantiate(shipView, contentArea);
    }

    private void DisplayEquipmentDetails(EquipmentPoint equipment) {
        ClearContentArea();
        EquipmentDetailsView equipmentDetails = Instantiate(equipmentDetailsView, contentArea);
        equipmentDetails.Set(equipment.Config);
        EquipmentListView equipmentList = Instantiate(equipmentListView, contentArea);
        equipmentList.DisplayLootForEquipmentSlot(equipment.Config.EquipSlot);
    }
    
    public void OnEquipmentClick (EquipmentPoint equipment) {
        DisplayEquipmentDetails(equipment);
    }
}
