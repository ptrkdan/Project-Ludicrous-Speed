using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentPoint : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI equipmentName;
    [SerializeField] EquipmentSlot equipSlot;

    Equipment equipment;

    public EquipmentSlot EquipSlot { get => equipSlot; }
    public Equipment Equipment { get => equipment; }

    private void SetIcon(Sprite icon) {
        this.icon.sprite = icon;
    }

    private void SetName(string equipmentName) {
        this.equipmentName.text = equipmentName;
    }

    public void SetInfo(Equipment equipment) {
        if (equipment != null)
        {
            this.equipment = equipment;
            SetIcon(equipment.GetIcon());
            SetName(equipment.GetName());
        }
    }
}