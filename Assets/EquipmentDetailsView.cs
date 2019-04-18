using UnityEngine;
using TMPro;

public class EquipmentDetailsView : OverlayView
{
    [Header("UI References")]
    [SerializeField] TextMeshProUGUI equipmentName;
    [SerializeField] TextMeshProUGUI hullValue;
    [SerializeField] TextMeshProUGUI shieldValue;
    [SerializeField] TextMeshProUGUI engineValue;
    [SerializeField] TextMeshProUGUI weaponValue;
    [SerializeField] TextMeshProUGUI auxValue;

    EquipmentConfig config;

    public void Set(EquipmentConfig config) {
        this.config = config;
        equipmentName.text = config.LootName;
        hullValue.text = config.GetStatModValue(StatType.Hull).ToString();
        shieldValue.text = config.GetStatModValue(StatType.Shield).ToString();
        engineValue.text = config.GetStatModValue(StatType.Engine).ToString();
        weaponValue.text = config.GetStatModValue(StatType.Weapon).ToString();
        auxValue.text = config.GetStatModValue(StatType.Aux).ToString();
    }
}
