using UnityEngine;
using TMPro;
using System;

public class ContractDetailsManager : MonoBehaviour
{
    [Header("Cached")]
    [SerializeField] GameSession session;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] ContractConfig contract;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI contractTitleText;
    [SerializeField] TextMeshProUGUI contractDetailsText;

    private void Awake() {
        session = FindObjectOfType<GameSession>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnEnable() {
        if (session) {
            SetContractConfig(session.ActiveContract);
        } else {
            throw new Exception("Game Session not set. This overlay may have been accidentally left enabled.");
        }
    }

    public void SetContractConfig(ContractConfig contract) {
        this.contract = contract;
        contractTitleText.text = contract.GetContractTitle();
        contractDetailsText.text = contract.GetContractDetails();
    }

    public void LoadContract() {
        sceneLoader.LoadSmugglingRunScene();
    }
}
