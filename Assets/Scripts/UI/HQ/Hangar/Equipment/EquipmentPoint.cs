using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentPoint : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI equipmentName;
    [SerializeField] EquipmentSlot equipSlot;

    EquipmentConfig config;

    public EquipmentSlot EquipSlot { get => equipSlot; }
    public EquipmentConfig Config { get => config; }

    private void SetIcon(Sprite icon) {
        this.icon.sprite = icon;
    }

    private void SetName(string equipmentName) {
        this.equipmentName.text = equipmentName;
    }

    public void SetInfo(EquipmentConfig config) {
        if (config) {
            this.config = config;
            SetIcon(config.Icon);
            SetName(config.LootName);
        }
    }
}

// On empty slots, it cannot retrieve EquipSlot enum