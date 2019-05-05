using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RunResultsManager : MonoBehaviour
{
    [Header("Cached")]
    [SerializeField] GameSession session;
    [SerializeField] PlayerSingleton player;
    [SerializeField] SceneLoader sceneLoader;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI resultsText;
    [SerializeField] Transform lootGrid;
    [SerializeField] Button restartButton;
    [SerializeField] Button hqButton;

    [Header("UI Prefabs")]
    [SerializeField] RunResultsLootRow lootRowPrefab;

    private void Start() {
        session = FindObjectOfType<GameSession>();
        if (!session) {
            sceneLoader.GoToPreload();
        }

        player = FindObjectOfType<PlayerSingleton>();
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
                RunResultsLootRow newLoot = Instantiate(lootRowPrefab, lootGrid);
                newLoot.DisplayLoot(lootList[i]);
                
                if (lootList[i].LootName == "Credits") {
                    player.AddToCredits(lootList[i].CreditValue);
                } else {
                    player.AddToInventory(lootList[i].Create());
                }
            }
        } else {
            // TODO: Give XP? Pay 10% of credit reward?
        }
    }
}
