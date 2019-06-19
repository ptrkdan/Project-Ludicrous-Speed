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

    [Header("For Testing")]
    [SerializeField] List<LootFactory> lootFactories;

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
    
    private void SetLoot(bool success)
    {
        if (success)
        {
            // Get LootFactory list from active contract

            // Get loot drop rate from active contract?

            // Loot rolls
            List<LootConfig> receivedRewards = new List<LootConfig>();
            foreach (LootFactory factory in lootFactories)
            {
                LootConfig droppedLoot = factory.DropLoot(LootRarity.Common, LootRarity.Rare);
                if (droppedLoot)
                {
                    receivedRewards.Add(droppedLoot);
                }
            }

            // TODO SortLoot(receivedRewards);

            for (int i = 0; i < receivedRewards.Count; i++)
            {
                RunResultsLootRow newLoot = Instantiate(lootRowPrefab, lootGrid);
                newLoot.DisplayLoot(receivedRewards[i]);

                if (receivedRewards[i].LootType == LootType.Currency)
                {
                    player.AddToCredits(receivedRewards[i].CreditValue);
                }
                else
                {
                    player.AddToInventory(receivedRewards[i].Create());
                }
            }
        } else
        {
            // TODO Penalty? Give reduced credit reward?
        }
    }
}
