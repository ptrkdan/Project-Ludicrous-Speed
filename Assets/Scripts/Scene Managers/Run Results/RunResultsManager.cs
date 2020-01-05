using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RunResultsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private Transform lootGrid;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button hqButton;

    [Header("UI Prefabs")]
    [SerializeField] private RunResultsLootRow lootRowPrefab;

    private GameSession session;
    private PlayerSingleton player;
    private SceneLoader sceneLoader;

    private void Start()
    {
        session = FindObjectOfType<GameSession>();
        if (!session)
        {
            sceneLoader.GoToPreload();
        }

        player = FindObjectOfType<PlayerSingleton>();
        FinalizeResults(session.IsRunSuccessful);
    }

    private void FinalizeResults(bool success)
    {
        SetResultsText(success);
        SetLoot(success);
        UpdatePlayerLevel(success);
        InvokeMissionComplete(success, session.ActiveContract);
    }

    private void SetResultsText(bool success)
    {
        if (success)
        {
            resultsText.text = "Successful!";
            restartButton.gameObject.SetActive(false);
            hqButton.transform.position -= new Vector3(120, 0); // TODO Make into const
        }
        else
        {
            resultsText.text = "Busted!";
        }
    }

    private void SetLoot(bool success)
    {
        if (success)
        {
            ContractConfig contract = session.ActiveContract;
            contract.Flags |= ContractFlags.isCompleted;

            // Get LootFactory list from active contract
            List<LootFactory> lootFactories = contract.LootDrops;

            // Special (guaranteed) loot drops
            List<LootConfig> receivedRewards = new List<LootConfig>();
            foreach (LootConfig loot in contract.SpecialLootDrops)
            {
                receivedRewards.Add(loot);
            }

            // Loot rolls
            foreach (LootFactory factory in lootFactories)
            {
                // TODO: Determine min and max rarity
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
        }
        else
        {
            // TODO Penalty? Give reduced credit reward?
        }
    }

    private void UpdatePlayerLevel(bool success)
    {
        // TODO Determine experience point reward for successful and failed missions
        if (success)
        {
            PlayerSingleton.instance.IncreaseExperiencePoints(100);
        }
    }

    private void InvokeMissionComplete(bool success, ContractConfig contract)
    {
        session.OnMissionCompleteCallback?.Invoke(success, contract);
        if (contract.ContractType == ContractType.Campaign)
        {
            session.OnCampaignMissionCompleteCallback?.Invoke(success, contract.ContractID);
        }
    }
}
