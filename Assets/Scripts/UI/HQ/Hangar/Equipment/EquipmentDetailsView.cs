using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentDetailsView : Overlay
{
    [Header("UI References")]
    [SerializeField] TextMeshProUGUI equipmentName;
    [SerializeField] TextMeshProUGUI hullValue;
    [SerializeField] TextMeshProUGUI shieldValue;
    [SerializeField] TextMeshProUGUI engineValue;
    [SerializeField] TextMeshProUGUI weaponValue;
    [SerializeField] TextMeshProUGUI auxValue;
    [SerializeField] Button equipButton;

    Equipment equipment;

    public void DisplayEquipmentDetails(Equipment equipment) {
        if (equipment == null) return;
        this.equipment = equipment;
        equipmentName.text = equipment.GetName();
        hullValue.text = equipment.GetStatModValue(StatType.Hull).ToString();
        shieldValue.text = equipment.GetStatModValue(StatType.Shield).ToString();
        engineValue.text = equipment.GetStatModValue(StatType.Engine).ToString();
        weaponValue.text = equipment.GetStatModValue(StatType.Weapon).ToString();
        auxValue.text = equipment.GetStatModValue(StatType.Aux).ToString();
        equipButton.gameObject.SetActive(true);
        if (equipment.IsEquipped()) {
            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip";
        } else {
            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }
    }

    private void ClearDetails() {
        equipmentName.text = "";
        hullValue.text = "";
        shieldValue.text = "";
        engineValue.text = "";
        weaponValue.text = "";
        auxValue.text = "";
        equipButton.gameObject.SetActive(false);
    }
}
