using UnityEngine;
using UnityEngine.UI;

public class ApartmentInventorySlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] InventoryDetailsView detailsView;
    [SerializeField] Image icon;

    [Space]
    [SerializeField] Loot loot;

    private void Start() {
        detailsView = FindObjectOfType<InventoryDetailsView>();
    }

    public void DisplayLoot(Loot newLoot) {
        loot = newLoot;
        icon.sprite = loot.GetIcon();
    }

    public void ClearSlot() {
        Destroy(gameObject);
    }

    public void DisplayLootDetails() {
        detailsView.DisplayLootDetails(loot);
    }
}
