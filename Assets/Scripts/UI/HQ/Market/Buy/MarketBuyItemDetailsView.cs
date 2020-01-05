using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketBuyItemDetailsView : Overlay
{
    [Header("UI References - Item Info")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;

    [Header("UI References - Item Stats")]
    [SerializeField] private RectTransform statsPanel;
    [SerializeField] private TextMeshProUGUI hullValue;
    [SerializeField] private TextMeshProUGUI shieldValue;
    [SerializeField] private TextMeshProUGUI engineValue;
    [SerializeField] private TextMeshProUGUI weaponValue;
    [SerializeField] private TextMeshProUGUI auxValue;

    [Header("UI References - Controls")]
    [SerializeField] private RectTransform controlsPanel;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button compareButton;
    [SerializeField] private Button buyAndEquipButton;

    [Space]
    [SerializeField] private int itemCostFactor = 4;

    private Loot item;

    private void Start()
    {
        ClearDetails();
    }

    public void DisplayLootDetails(Loot item)
    {
        ClearDetails();
        this.item = item;
        itemName.text = item.GetName();
        itemCost.text = $"{item.GetCreditValue() * itemCostFactor} Cr";
        itemDescription.text = item.GetDescription();
        itemIcon.gameObject.SetActive(true);
        itemIcon.GetComponentsInChildren<Image>()[1].sprite = item.GetIcon();
        controlsPanel.gameObject.SetActive(true);
        TextMeshProUGUI buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
        buyButtonText.text = "Buy";
        buyButtonText.color = new Color(0, 0, 0);
        if (item.GetLootType() == LootType.Equipment)
        {
            Equipment equipment = item as Equipment;
            compareButton.gameObject.SetActive(true);
            buyAndEquipButton.gameObject.SetActive(true);
            statsPanel.gameObject.SetActive(true);
            hullValue.text = equipment.GetStatModValue(StatType.Hull).ToString();
            shieldValue.text = equipment.GetStatModValue(StatType.Shield).ToString();
            engineValue.text = equipment.GetStatModValue(StatType.Engine).ToString();
            weaponValue.text = equipment.GetStatModValue(StatType.Weapon).ToString();
            auxValue.text = equipment.GetStatModValue(StatType.Aux).ToString();
        }
        else
        {
            compareButton.gameObject.SetActive(false);
            buyAndEquipButton.gameObject.SetActive(false);
        }
    }

    public void OnBuy()
    {
        InventoryManager inventory = InventoryManager.instance;
        int itemCost = item.GetCreditValue() * itemCostFactor;
        if (inventory.Credits >= itemCost)
        {
            inventory.DeductFromCredits(itemCost);
            inventory.RemoveFromMarketInventory(item);
            inventory.AddToPlayerInventory(item);
            ClearDetails();
            Debug.Log($"{item.GetName()} purchased for ${itemCost}");
        }
        else
        {
            TextMeshProUGUI buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
            buyButtonText.text = "Not enough credits";
            buyButtonText.color = new Color(1, 0, 0);
        }
    }

    public void OnCompare()
    {
        Debug.Log($"Comparing {item.GetName()} with currently equipped item ");
    }

    public void OnBuyAndEquip()
    {
        Debug.Log($"{item.GetName()} is purchased and equipped");

    }

    private void ClearDetails()
    {
        itemName.text = "";
        itemCost.text = "";
        itemDescription.text = "";
        itemIcon.gameObject.SetActive(false);
        statsPanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(false);
    }
}
