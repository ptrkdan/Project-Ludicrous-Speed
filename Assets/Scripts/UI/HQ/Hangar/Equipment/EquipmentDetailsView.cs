using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentDetailsView : Overlay
{
    [Header("UI References")]
    [SerializeField] private Image equipmentIcon;
    [SerializeField] private TextMeshProUGUI equipmentName;
    [SerializeField] private TextMeshProUGUI hullValue;
    [SerializeField] private TextMeshProUGUI shieldValue;
    [SerializeField] private TextMeshProUGUI engineValue;
    [SerializeField] private TextMeshProUGUI weaponValue;
    [SerializeField] private TextMeshProUGUI auxValue;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button unequipButton;

    private Equipment equipment;
    public void DisplayEquipmentDetails(Equipment equipment)
    {
        if (equipment == null) return;
        ClearDetails();
        this.equipment = equipment;
        equipmentIcon.sprite = equipment.GetIcon();
        equipmentName.text = equipment.GetName() +
            (equipment.IsEquipped ? " (Equipped)" : null);
        hullValue.text = equipment.GetStatModValue(StatType.Hull).ToString();
        shieldValue.text = equipment.GetStatModValue(StatType.Shield).ToString();
        engineValue.text = equipment.GetStatModValue(StatType.Engine).ToString();
        weaponValue.text = equipment.GetStatModValue(StatType.Weapon).ToString();
        auxValue.text = equipment.GetStatModValue(StatType.Aux).ToString();
        if (!equipment.IsDefault)
        {
            if (equipment.IsEquipped)
            {
                equipButton.gameObject.SetActive(false);
                unequipButton.gameObject.SetActive(true);
            }
            else
            {
                equipButton.gameObject.SetActive(true);
                unequipButton.gameObject.SetActive(false);

            }
        }
    }

    private void ClearDetails()
    {
        equipmentName.text = "";
        hullValue.text = "";
        shieldValue.text = "";
        engineValue.text = "";
        weaponValue.text = "";
        auxValue.text = "";
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
    }

    public void Equip()
    {
        equipment.Use();
        DisplayEquipmentDetails(equipment);
    }

    public void Unequip()
    {
        EquipmentManager.instance.UnEquip((int)equipment.GetEquipSlot());
        DisplayEquipmentDetails(equipment);
    }
}
