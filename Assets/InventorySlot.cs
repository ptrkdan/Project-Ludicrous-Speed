using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] InventoryDetailsView detailsView;
    [SerializeField] Image lootImage;

    [Space]
    [SerializeField] LootConfig config;

    private void Start() {
        detailsView = FindObjectOfType<InventoryDetailsView>();
    }

    public void DisplayLoot(LootConfig newLoot) {
        config = newLoot;
        lootImage.sprite = config.LootSprite;


    }

    public void DisplayLootDetails() {
        Debug.Log($"Displaying details for {config.LootName}");
        detailsView.DisplayLootDetails(config);
    }
}
