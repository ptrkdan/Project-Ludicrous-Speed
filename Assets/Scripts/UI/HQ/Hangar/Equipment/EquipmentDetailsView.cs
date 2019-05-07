using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentDetailsView : Overlay
{
    [Header("UI References")]
    [SerializeField] Image equipmentIcon;
    [SerializeField] TextMeshProUGUI equipmentName;
    [SerializeField] TextMeshProUGUI hullValue;
    [SerializeField] TextMeshProUGUI shieldValue;
    [SerializeField] TextMeshProUGUI engineValue;
    [SerializeField] TextMeshProUGUI weaponValue;
    [SerializeField] TextMeshProUGUI auxValue;
    [SerializeField] Button equipButton;
    [SerializeField] Button unequipButton;

    Equipment equipment;

    public void DisplayEquipmentDetails(Equipment equipment) {
        if (equipment == null) return;
        this.equipment = equipment;
        equipmentIcon.sprite = equipment.GetIcon();
        equipmentName.text = equipment.GetName();
        hullValue.text = equipment.GetStatModValue(StatType.Hull).ToString();
        shieldValue.text = equipment.GetStatModValue(StatType.Shield).ToString();
        engineValue.text = equipment.GetStatModValue(StatType.Engine).ToString();
        weaponValue.text = equipment.GetStatModValue(StatType.Weapon).ToString();
        auxValue.text = equipment.GetStatModValue(StatType.Aux).ToString();
        equipButton.gameObject.SetActive(true);
        if (equipment.IsEquipped) {
            equipButton.gameObject.SetActive(false);
            unequipButton.gameObject.SetActive(true);
        } else {
            equipButton.gameObject.SetActive(true);
            unequipButton.gameObject.SetActive(false);

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

    public void Equip()
    {
        equipment.Use();
    }

    public void Unequip()
    {
        EquipmentManager.instance.UnEquip((int)equipment.GetEquipSlot());
    }
}
