using UnityEngine;

public class HangarOverlay : Overlay
{
    [Header("UI References")]
    [SerializeField] Transform contentArea;

    [Header("UI Prefabs")]
    [SerializeField] HangarShipView shipView;
    [SerializeField] EquipmentListView equipmentListView;
    [SerializeField] EquipmentDetailsView equipmentDetailsView;

    PlayerSingleton player;
    bool isDisplayingEquipmentDetail = false;

    public override void Display() {
        base.Display();
    }

    private void OnEnable() {
        player = FindObjectOfType<PlayerSingleton>();
        DisplayShipView();
    }


    private void ClearContentArea() {
        foreach (Overlay view in contentArea.GetComponentsInChildren<Overlay>()) {
            Destroy(view.gameObject);
        }
    }

    private void DisplayShipView() {
        ClearContentArea();
        HangarShipView ship = Instantiate(shipView, contentArea);
        isDisplayingEquipmentDetail = false;
    }

    private void DisplayEquipmentDetails(EquipmentPoint equipPoint) {
        ClearContentArea();
        EquipmentDetailsView equipmentDetails = Instantiate(equipmentDetailsView, contentArea);
        equipmentDetails.DisplayEquipmentDetails(equipPoint.Equipment);
        EquipmentListView equipmentList = Instantiate(equipmentListView, contentArea);
        equipmentList.DisplayLootForEquipmentSlot(equipPoint.EquipSlot);
        isDisplayingEquipmentDetail = true;
    }
    
    public void OnEquipmentClick (EquipmentPoint equipment) {
        DisplayEquipmentDetails(equipment);
    }

    public override void GoBack() {
        if (isDisplayingEquipmentDetail) {
            DisplayShipView();
        } else {
            base.GoBack();
        }
    }
}
