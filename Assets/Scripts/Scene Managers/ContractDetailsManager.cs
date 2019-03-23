using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContractDetailsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI contractTitleText;
    [SerializeField] TextMeshProUGUI contractDetailsText;

    [SerializeField] GameSession session;
    [SerializeField] SceneLoader sceneLoader;

    [SerializeField] ContractConfig contractConfig;

    private void Awake() {
        session = FindObjectOfType<GameSession>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnEnable() {
        SetContractConfig(session.ActiveContract);
    }

    public void SetContractConfig(ContractConfig contract) {
        contractConfig = contract;
        contractTitleText.text = contract.GetContractTitle();
        contractDetailsText.text = contract.GetContractDetails();
    }

    public void LoadContract() {
        sceneLoader.LoadSmugglingRunScene();
    }
}
