using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContractDetailsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI contractTitleText;
    [SerializeField] TextMeshProUGUI contractDetailsText;
    [SerializeField] SceneLoader sceneLoader;

    [SerializeField] ContractConfig contractConfig;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void SetContractConfig(ContractConfig contract) {
        contractConfig = contract;
        contractTitleText.text = contract.GetContractTitle();
        contractDetailsText.text = contract.GetContractDetails();
    }

    public void LoadContract() {
        sceneLoader.LoadSmugglingRunScene(contractConfig);
    }
}
