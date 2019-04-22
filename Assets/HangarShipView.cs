using UnityEngine;

public class HangarShipView : OverlayView
{
    [Header("Equipment Points")]
    [SerializeField] EquipmentPoint primaryWpnPoint;
    [SerializeField] EquipmentPoint secondaryWpnPoint;
    [SerializeField] EquipmentPoint supportPoint;
    [SerializeField] EquipmentPoint primaryModPoint;
    [SerializeField] EquipmentPoint secondaryModPoint;

    PlayerSingleton player;

    private void OnEnable() {
        player = FindObjectOfType<PlayerSingleton>();
        SetEquipInfo();
    }

    private void SetEquipInfo() {
        primaryWpnPoint.SetInfo(player.GetEquipment(EquipmentSlot.PrimaryWeapon));
        secondaryWpnPoint.SetInfo(player.GetEquipment(EquipmentSlot.SecondaryWeapon));
        supportPoint.SetInfo(player.GetEquipment(EquipmentSlot.Support));
        primaryModPoint.SetInfo(player.GetEquipment(EquipmentSlot.PrimaryMod));
        secondaryModPoint.SetInfo(player.GetEquipment(EquipmentSlot.SecondaryMod));
    }

    public void OnEquipmentClick(EquipmentPoint equipment) {
        // HangarManager.instance.OnEquipmentClick(equipment);
    }
}
