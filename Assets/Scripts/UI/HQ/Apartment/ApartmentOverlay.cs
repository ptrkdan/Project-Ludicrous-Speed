using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApartmentOverlay : Overlay
{
    private const string PILOT_TAB = "Pilot Tab Button";
    private const string SHIP_TAB = "Ship Tab Button";
    private const string INVENTORY_TAB = "Inventory Tab Button";

    [Header("Cached")]
    [SerializeField] private PlayerSingleton player;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private Slider playerExpBar;
    [SerializeField] private RectTransform contentArea;

    [Header("UI Prefabs")]
    [SerializeField] private CareerView careerView;
    [SerializeField] private Overlay perksView;
    [SerializeField] private ShipView shipView;
    [SerializeField] private StatsView statsView;
    [SerializeField] private InventoryListView inventoryListView;
    [SerializeField] private InventoryDetailsView inventoryDetailsView;
    [SerializeField] private CreditsView creditsView;

    [Space]
    [SerializeField] private string activeTab;

    public string ActiveTab { get => activeTab; set => activeTab = value; }

    public override void Display()
    {
        base.Display();
    }

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerSingleton>();
        SetPlayerInfo();
        ChangeTab(PILOT_TAB);
    }

    private void SetPlayerInfo()
    {
        playerNameText.text = player.PlayerName;
        playerLevelText.text = $"LVL {player.PlayerLevel.ToString()}";
        playerExpBar.value = (float)player.ExperiencePoints / 100;             // TODO: Change percentage based on level
    }

    private void ChangeTab(string nextTab)
    {
        ClearContentArea();
        switch (nextTab)
        {
            case PILOT_TAB:
                CareerView career = Instantiate(careerView, contentArea);
                // TODO: career.SetCareerIcon = ???
                career.SetPlayerTitle(player.Title);
                career.SetPlayerTitleDescription(player.TitleDescription);

                Instantiate(perksView, contentArea);
                break;
            case SHIP_TAB:
                Instantiate(shipView, contentArea);
                Instantiate(statsView, contentArea);
                break;
            case INVENTORY_TAB:
                Instantiate(inventoryListView, contentArea);
                Instantiate(inventoryDetailsView, contentArea);
                Instantiate(creditsView, contentArea);
                break;
            default:
                break;
        }
        activeTab = nextTab;
    }

    private void ClearContentArea()
    {
        foreach (Overlay view in contentArea.GetComponentsInChildren<Overlay>())
        {
            Destroy(view.gameObject);
        }
    }

    public void OnTabButtonClick(Button tabButton)
    {
        string nextTab = tabButton.name;
        if (activeTab != nextTab)
        {
            ChangeTab(nextTab);
        }
    }
}
