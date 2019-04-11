using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] InventoryDetailsView detailsView;
    [SerializeField] Image icon;

    [Space]
    [SerializeField] LootConfig config;

    private void Start() {
        detailsView = FindObjectOfType<InventoryDetailsView>();
    }

    public void DisplayLoot(LootConfig newLoot) {
        config = newLoot;
        icon.sprite = config.Icon;
    }

    public void ClearSlot() {
        Destroy(gameObject);
    }

    public void DisplayLootDetails() {
        Debug.Log($"Displaying details for {config.LootName}");
        detailsView.DisplayLootDetails(config);
    }
}
