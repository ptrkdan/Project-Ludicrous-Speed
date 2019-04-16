using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentPoint : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name;

    private void SetIcon(Sprite icon) {
        this.icon.sprite = icon;
    }

    private void SetName(string name) {
        this.name.text = name;
    }

    public void SetInfo(EquipmentConfig equipment) {
        if (equipment) {
            SetIcon(equipment.Icon);
            SetName(equipment.LootName);
        }
    }
}
