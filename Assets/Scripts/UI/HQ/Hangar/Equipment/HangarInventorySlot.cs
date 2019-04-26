using UnityEngine;
using UnityEngine.UI;

public class HangarInventorySlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] EquipmentDetailsView detailsView;
    [SerializeField] Image icon;

    [Space]
    [SerializeField] EquipmentConfig config;

    private void Start() {
        detailsView = FindObjectOfType<EquipmentDetailsView>();
    }

    public void DisplayLoot(EquipmentConfig newLoot) {
        config = newLoot;
        icon.sprite = config.Icon;
    }

    public void ClearSlot() {
        Destroy(gameObject);
    }

    public void DisplayLootDetails() {
        detailsView.DisplayEquipmentDetails(config);
    }
}
