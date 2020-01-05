using UnityEngine;

public class HangarShipView : Overlay
{

    [Header("Equipment Points")]
    [SerializeField] private EquipmentPoint primaryWpnPoint;
    [SerializeField] private EquipmentPoint secondaryWpnPoint;
    [SerializeField] private EquipmentPoint supportPoint;
    [SerializeField] private EquipmentPoint primaryModPoint;
    [SerializeField] private EquipmentPoint secondaryModPoint;

    private PlayerSingleton player;
    private HangarOverlay hangarManager;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerSingleton>();
        hangarManager = FindObjectOfType<HangarOverlay>();
        SetEquipInfo();
    }

    private void SetEquipInfo()
    {
        primaryWpnPoint.SetInfo(player.GetEquipment(EquipmentSlot.PrimaryWeapon));
        secondaryWpnPoint.SetInfo(player.GetEquipment(EquipmentSlot.SecondaryWeapon));
        supportPoint.SetInfo(player.GetEquipment(EquipmentSlot.Support));
        primaryModPoint.SetInfo(player.GetEquipment(EquipmentSlot.PrimaryMod));
        secondaryModPoint.SetInfo(player.GetEquipment(EquipmentSlot.SecondaryMod));
    }

    public void OnEquipmentClick(EquipmentPoint equipment)
    {
        hangarManager.OnEquipmentClick(equipment);
    }
}
