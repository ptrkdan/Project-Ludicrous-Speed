using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gates/Campaign Contract Gate")]
public class CampaignContractGate : ContractGate
{
    [SerializeField] private CampaignContractPool campaignContractPool;

    public override List<ContractConfig> GetContracts()
    {
        return campaignContractPool.GetContracts();
    }

    public override void UpdatePlayerPrereqStatus(ContractPrereq playerPrereqStatus)
    {
        campaignContractPool.UpdatePrereqStatus(playerPrereqStatus);
    }

    public void ResetCampaign()
    {
        campaignContractPool.ResetCampaign();
    }

    public override void ResetAllContractFlags()
    {
        campaignContractPool.ResetFlags();
    }
}
