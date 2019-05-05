using UnityEngine;
using UnityEngine.UI;

public class HangarInventorySlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] EquipmentDetailsView detailsView;
    [SerializeField] Image icon;

    [Space]
    [SerializeField] Equipment equipment;

    private void Start() {
        detailsView = FindObjectOfType<EquipmentDetailsView>();
    }

    public void DisplayLoot(Equipment newLoot) {
        equipment = newLoot;
        icon.sprite = equipment.GetIcon();
    }

    public void ClearSlot() {
        Destroy(gameObject);
    }

    public void DisplayLootDetails() {
        detailsView.DisplayEquipmentDetails(equipment);
    }
}
