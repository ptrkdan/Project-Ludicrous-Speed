using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ApartmentOverlay : Overlay {
    const string PILOT_TAB = "Pilot Tab Button";
    const string SHIP_TAB = "Ship Tab Button";
    const string INVENTORY_TAB = "Inventory Tab Button";

    [Header("Cached")]
    [SerializeField] PlayerSingleton player;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI playerLevelText;
    [SerializeField] Slider playerExpBar;
    [SerializeField] RectTransform contentArea;

    [Header("UI Prefabs")]
    [SerializeField] CareerView careerView;
    [SerializeField] Overlay perksView;
    [SerializeField] ShipView shipView;
    [SerializeField] StatsView statsView;
    [SerializeField] InventoryListView inventoryListView;
    [SerializeField] InventoryDetailsView inventoryDetailsView;
    [SerializeField] CreditsView creditsView;

    [Space]
    [SerializeField] string activeTab;

    public string ActiveTab { get => activeTab; set => activeTab = value; }

    public override void Display() {
        base.Display();
    }

    private void OnEnable() {
        player = FindObjectOfType<PlayerSingleton>();
        SetPlayerInfo();
        ChangeTab(PILOT_TAB);
    }

    private void SetPlayerInfo() {
        playerNameText.text = player.PlayerName;
        playerLevelText.text = $"LVL {player.PlayerLevel.ToString()}";
        playerExpBar.value = (float)player.ExperiencePoints / 100;             // TODO: Change percentage based on level
    }

    private void ChangeTab(string nextTab) {
        ClearContentArea();
        switch (nextTab) {
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

    private void ClearContentArea() {
        foreach (Overlay view in contentArea.GetComponentsInChildren<Overlay>()) {
            Destroy(view.gameObject);
        }
    }

    public void OnTabButtonClick(Button tabButton) {
        string nextTab = tabButton.name;
        if (activeTab != nextTab) {
            ChangeTab(nextTab);
        }
    }
}
