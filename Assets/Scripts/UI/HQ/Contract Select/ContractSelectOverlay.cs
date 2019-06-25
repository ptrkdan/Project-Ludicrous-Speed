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
    [SerializeField] [Range(1, 3)] int displayedContractCount = 3;
    MasterContractGate masterContractGate;

    private bool contractsDisplayed = false;

    public override void Display()
    {
        base.Display();
    }

    private void OnEnable()
    {
        masterContractGate = ContractManager.instance.GetMasterContractGate();
        if (!contractsDisplayed)
        {      // TODO Refresh on specified time? After certain criteria, such as contract complete?
            DisplayAllContracts();
        }
    }

    private void DisplayAllContracts()
    {
        List<ContractConfig> availableContracts = new List<ContractConfig>(masterContractGate.GetContracts());
        for (int i = 0; i < displayedContractCount; i++)
        {
            ContractConfig contractConfig = SelectFromContractConfigList(availableContracts);
            DisplayContract(contractConfig, i);
        }
    }

    private ContractConfig SelectFromContractConfigList(List<ContractConfig> contracts)
    {
        ContractConfig config = contracts[Random.Range(0, contracts.Count)];
        contracts.Remove(config);

        return config;
    }

    private void DisplayContract(ContractConfig contract, int index)
    {
        ContractSelection newContractSelection = Instantiate(contractSelectionPrefab, gameObject.transform);
        newContractSelection.transform.localPosition
            = new Vector3(contractSelectXOffset, contractSelectYOffset - (index * contractSelectHeight));
        newContractSelection.SetContractConfig(contract);
    }
}
