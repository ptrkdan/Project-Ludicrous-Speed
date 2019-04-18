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
        ClearContentArea();
        DisplayShipView();
    }


    private void ClearContentArea() {
        foreach (OverlayView view in contentArea.GetComponentsInChildren<OverlayView>()) {
            Destroy(view.gameObject);
        }
    }

    private void DisplayShipView() {
        HangarShipView ship = Instantiate(shipView, contentArea);
    }

    // TODO: Move this to HangarShipView
    public void OnEquipmentClick (EquipmentPoint equipment) {
        EquipmentDetailsView equipmentDetails = Instantiate(equipmentDetailsView, contentArea);
        equipmentDetails.Set(equipment.Config);     
    }
}
