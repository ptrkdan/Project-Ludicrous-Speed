using System;
using TMPro;
using UnityEngine;

public class ContractDetailsOverlay : Overlay
{

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI contractTitleText;
    [SerializeField] private TextMeshProUGUI contractDetailsText;
    [SerializeField] private Transform lootLabel;
    [SerializeField] private Transform difficultyLabel;
    [SerializeField] private Transform lootList;
    [SerializeField] private Transform specialLootList;

    [Header("UI Prefabs")]
    [SerializeField] private ContractLootSlot contractLootSlotPrefab;
    private GameSession session;
    private SceneLoader sceneLoader;
    private ContractConfig contract;

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
