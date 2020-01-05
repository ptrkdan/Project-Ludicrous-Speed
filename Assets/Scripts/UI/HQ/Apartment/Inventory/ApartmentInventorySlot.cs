using UnityEngine;
using UnityEngine.UI;

public class ApartmentInventorySlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private InventoryDetailsView detailsView;
    [SerializeField] private Image icon;

    [Space]
    [SerializeField] private Loot loot;

    private void Start()
    {
        detailsView = FindObjectOfType<InventoryDetailsView>();
    }

    public void DisplayLoot(Loot newLoot)
    {
        loot = newLoot;
        icon.sprite = loot.GetIcon();
    }

    public void ClearSlot()
    {
        Destroy(gameObject);
    }

    public void DisplayLootDetails()
    {
        detailsView.DisplayLootDetails(loot);
    }
}
