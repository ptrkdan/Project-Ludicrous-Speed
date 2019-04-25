using System.Collections.Generic;
using UnityEngine;

public class ContractSelectOverlay : Overlay
{
    [Header("UI Prefabs")]
    [SerializeField] ContractSelection contractSelectionPrefab;

    [Header("UI Offsets")]
    [SerializeField] float contractSelectXOffset = 0;
    [SerializeField] float contractSelectYOffset = 60;
    [SerializeField] float contractSelectHeight = 100;

    [Header("Contract Data")]
    [SerializeField] [Range(1,3)] int displayedContractCount = 3;
    [SerializeField] List<ContractConfig> contractConfigList;

    private bool contractsDisplayed = false;

    public override void Display() {
        base.Display();
    }

    private void OnEnable() {
        if (!contractsDisplayed) {
            DisplayAllContracts();
        }
    }

    private void DisplayAllContracts() {
        for (int i = 0; i < displayedContractCount; i ++) {
            // Take one contract from config list.
            ContractConfig contractConfig = SelectFromContractConfigList(contractConfigList);
            // Display
            DisplayContract(contractConfig, i);
        }
        contractsDisplayed = true;
    }

    private ContractConfig SelectFromContractConfigList(List<ContractConfig> contracts) {
        // TODO: Create a better algorithm for randomization
        ContractConfig config = contracts[Random.Range(0, contracts.Count)];
        contracts.Remove(config);

        return config;
    }

    private void DisplayContract(ContractConfig contract, int index) {
        ContractSelection newContractSelection = Instantiate(contractSelectionPrefab, gameObject.transform);
        newContractSelection.transform.localPosition 
            = new Vector3(contractSelectXOffset, contractSelectYOffset - (index * contractSelectHeight));
        newContractSelection.SetContractConfig(contract);
    }
}
