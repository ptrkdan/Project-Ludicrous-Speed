using UnityEngine;
using UnityEngine.UI;

public class MarketBuyItemSlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private MarketBuyItemDetailsView detailsView;
    [SerializeField] private Image icon;

    private Loot loot;

    private void Start()
    {
        detailsView = FindObjectOfType<MarketBuyItemDetailsView>();
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
