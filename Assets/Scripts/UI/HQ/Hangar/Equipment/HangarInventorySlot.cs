using UnityEngine;
using UnityEngine.UI;

public class HangarInventorySlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private EquipmentDetailsView detailsView;
    [SerializeField] private Image icon;

    [Space]
    [SerializeField] private Equipment equipment;

    private void Start()
    {
        detailsView = FindObjectOfType<EquipmentDetailsView>();
    }

    public void DisplayLoot(Equipment newLoot)
    {
        equipment = newLoot;
        icon.sprite = equipment.GetIcon();
    }

    public void ClearSlot()
    {
        Destroy(gameObject);
    }

    public void DisplayLootDetails()
    {
        detailsView.DisplayEquipmentDetails(equipment);
    }
}
