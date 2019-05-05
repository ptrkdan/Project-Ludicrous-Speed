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

    public void SetInfo(Equipment equipment
        ) {
        if (this.equipment != null) {
            SetIcon(this.equipment.GetIcon());
            SetName(this.equipment.GetName());
        }
    }
}

// On empty slots, it cannot retrieve EquipSlot enum