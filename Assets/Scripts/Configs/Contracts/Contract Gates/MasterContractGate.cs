using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gates/Master Contract Gate")]
public class MasterContractGate : ContractGate
{
    [SerializeField] CampaignContractGate campaignContractGate;
    [SerializeField] StandardContractGate standardContractGate;
    //[SerializeField] SpecialContractGate specialContractGate;

    List<ContractConfig> unlockedContracts = new List<ContractConfig>();
    bool isDirty = true;

    public override List<ContractConfig> GetContracts()
    {
        if (isDirty || unlockedContracts.Count == 0)
            UpdateUnlockedContracts();
        return unlockedContracts;
    }

    public override void UpdatePlayerPrereqStatus(ContractPrereq playerPrereqStatus)
    {
        campaignContractGate.UpdatePlayerPrereqStatus(playerPrereqStatus);
        standardContractGate.UpdatePlayerPrereqStatus(playerPrereqStatus);
        //specialContractGate.UpdatePrereqs(playerStatus);

        isDirty = true;
    }

    private void UpdateUnlockedContracts()
    {
        unlockedContracts.Clear();
        unlockedContracts.AddRange(campaignContractGate.GetContracts());
        unlockedContracts.AddRange(standardContractGate.GetContracts());
        //unlockedContracts.AddRange(specialContractGate.GetContracts());

        isDirty = false;
    }
}
