using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContractDetailsOverlay : Overlay
{

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI contractTitleText;
    [SerializeField] TextMeshProUGUI contractDetailsText;
    [SerializeField] Transform lootLabel;
    [SerializeField] Transform difficultyLabel;
    [SerializeField] Transform lootList;
    [SerializeField] Transform specialLootList;

    [Header("UI Prefabs")]
    [SerializeField] ContractLootSlot contractLootSlotPrefab;

    GameSession session;
    SceneLoader sceneLoader;
    ContractConfig contract;

    private void Awake()
    {
        session = FindObjectOfType<GameSession>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnEnable()
    {
        if (session)
        {
            SetContractConfig(session.ActiveContract);
        }
        else
        {
            throw new Exception("Game Session not set. This overlay may have been accidentally left enabled.");
        }
    }

    public void SetContractConfig(ContractConfig contract)
    {
        this.contract = contract;
        contractTitleText.text = contract.ContractTitle;
        contractDetailsText.text = contract.ContractDetails;

        for (int i = 0; i < contract.LootLevel; i++)
        {
            lootLabel.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < contract.DifficultyLevel; i++)
        {
            difficultyLabel.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < contract.LootDrops.Count; i++)
        {
            ContractLootSlot newSlot = Instantiate(contractLootSlotPrefab, lootList.transform);
            newSlot.SetIcon(contract.LootDrops[i].Icon);
        }
        for (int i = 0; i < contract.SpecialLootDrops.Count; i++)
        {
            ContractLootSlot newSlot = Instantiate(contractLootSlotPrefab, specialLootList.transform);
            newSlot.SetIcon(contract.SpecialLootDrops[i].Icon);
        }
    }

    public void LoadContract()
    {
        sceneLoader.LoadSmugglingRunScene();
    }
}
