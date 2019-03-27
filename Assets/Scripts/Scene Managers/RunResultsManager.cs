using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RunResultsManager : MonoBehaviour
{
    [Header("Cached")]
    [SerializeField] GameSession session;
    [SerializeField] SceneLoader sceneLoader;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI resultsText;
    [SerializeField] Transform lootPanel;
    [SerializeField] Scrollbar lootPanelScrollbar;
    [SerializeField] Button restartButton;
    [SerializeField] Button hqButton;

    [Header("UI Prefabs")]
    [SerializeField] GameObject lootIcon;

    [Header("UI Offsets")]
    [SerializeField] int lootIconPaddingX = 10;
    [SerializeField] int lootIconPaddingY = 10;
    [SerializeField] int lootIconOffsetY = 70;
    [SerializeField] int lootPanelWidth = 370;
    [SerializeField] int lootPanelHeightPerLoot = 75;

    private void Start() {
        session = FindObjectOfType<GameSession>();
        if (!session) {
            sceneLoader.GoToPreload();
        }

        FinalizeResults(session.IsRunSuccessful);
    }

    private void FinalizeResults(bool success) {
        SetResultsText(success);
        SetLoot(success);
    }

    private void SetResultsText(bool success) {
        if(success) {
            resultsText.text = "Successful!";
            restartButton.gameObject.SetActive(false);
            hqButton.transform.position -= new Vector3(120, 0); // TODO Make into const
        } else {
            resultsText.text = "Busted!";
        }
    }

    private void SetLoot(bool success) {
        List<LootConfig> lootList = session.ActiveContract.GetContractRewards();
        if (success) {
            for (int i = 0; i < lootList.Count; i++) {
                GameObject newLoot = Instantiate(lootIcon, lootPanel);
                newLoot.transform.Translate(
                    lootIconPaddingX, 
                    -lootIconPaddingY - (lootIconOffsetY*i), 
                    0);
                newLoot.GetComponent<Image>().sprite = lootList[i].LootSprite;
                newLoot.GetComponentInChildren<TextMeshProUGUI>().text = lootList[i].LootName;
                
                if (lootList[i].LootName == "Credits") {
                    session.Player.AddToCredits(lootList[i].LootValue);
                } else {
                    session.Player.AddToInventory(lootList[i]);
                }
            }
        } else {
            // TODO: Give XP? Pay 10% of credit reward?
        }

        lootPanel.GetComponent<RectTransform>().sizeDelta 
            = new Vector2(lootPanelWidth, lootPanelHeightPerLoot * (lootList.Count));
        lootPanelScrollbar.value = 0; // FIXME: scrollbar still shows up in the middle
    }
}
