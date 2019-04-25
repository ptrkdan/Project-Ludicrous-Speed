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
