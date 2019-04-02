using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ApartmentManager : MonoBehaviour {
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
    [SerializeField] OverlayView perksView;
    [SerializeField] InventoryView inventoryView;
    [SerializeField] CreditsView creditsView;

    [Space]
    [SerializeField] string activeTab;

    public string ActiveTab { get => activeTab; set => activeTab = value; }

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

    private void ClearContentArea() {
        foreach (OverlayView view in contentArea.GetComponentsInChildren<OverlayView>()) {
            view.gameObject.SetActive(false);
        }
    }

    private void ChangeTab(string nextTab) {
        switch (nextTab) {
            case PILOT_TAB:
                Debug.Log("Changed to Pilot Tab");
                CareerView career = Instantiate(careerView, contentArea);
                // TODO: career.SetCareerIcon = ???
                career.SetPlayerTitle(player.Title);
                career.SetPlayerTitleDescription(player.TitleDescription);

                Instantiate(perksView, contentArea);
                break;
            case SHIP_TAB:
                Debug.Log("Changed to Ship Tab");
                break;
            case INVENTORY_TAB:
                Debug.Log("Changed to Inventory Tab");
                InventoryView inventory = Instantiate(inventoryView, contentArea);
                CreditsView credits = Instantiate(creditsView, contentArea);
                credits.SetCreditsText(player.Credits);
                break;
            default:
                break;
        }
        activeTab = nextTab;
    }

    public void OnTabButtonClick(Button tabButton) {
        string nextTab = tabButton.name;
        if (activeTab != nextTab) {
            ClearContentArea();
            ChangeTab(nextTab);
        }
    }
}
