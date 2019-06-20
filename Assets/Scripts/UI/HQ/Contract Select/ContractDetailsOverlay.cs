using System;
using TMPro;
using UnityEngine;

public class ContractDetailsOverlay : Overlay
{

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI contractTitleText;
    [SerializeField] TextMeshProUGUI contractDetailsText;
    [SerializeField] Transform lootLabel;
    [SerializeField] Transform difficultyLabel;

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
    }

    public void LoadContract()
    {
        sceneLoader.LoadSmugglingRunScene();
    }
}
