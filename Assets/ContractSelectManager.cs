using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContractSelectManager : MonoBehaviour
{
    [SerializeField] [Range(1,3)] int displayedContractCount = 3;
    [SerializeField] List<ContractConfig> contractConfigList;
    [SerializeField] ContractSelection contractSelectionPrefab;


    private void OnEnable() {
        DisplayAllContracts();
    }

    private void DisplayAllContracts() {
        for (int i = 0; i < displayedContractCount; i ++) {
            // Take one contract from config list.
            ContractConfig contractConfig = SelectFromContractConfigList(contractConfigList);
            // Display
            DisplayContract(contractConfig, i);
        }
    }

    private ContractConfig SelectFromContractConfigList(List<ContractConfig> contracts) { 
        ContractConfig config = contracts[Random.Range(0, contracts.Count)];
        contracts.Remove(config);

        return config;
    }

    private void DisplayContract(ContractConfig contractConfig, int index) {
        ContractSelection newContractSelection = Instantiate(
            contractSelectionPrefab, 
            gameObject.transform);
        newContractSelection.transform.localPosition = new Vector3(0, 60 - (index * 100));
        newContractSelection.SetContractTitle(contractConfig.GetContractTitle());
        newContractSelection.SetLootLevel(contractConfig.GetContractLootLevel());
        newContractSelection.SetDifficultyLevel(contractConfig.GetContractDifficultyLevel());
    }
}
