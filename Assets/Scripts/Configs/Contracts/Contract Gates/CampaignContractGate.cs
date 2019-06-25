﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gates/Campaign Contract Gate")]
public class CampaignContractGate : ContractGate
{
    [SerializeField] CampaignContractPool campaignContractPool;

    public override List<ContractConfig> GetContracts()
    {
        return campaignContractPool.GetContracts();
    }

    public override void UpdatePlayerPrereqStatus(ContractPrereq playerPrereqStatus)
    {
        ResetCampaign();
        campaignContractPool.UpdatePrereqStatus(playerPrereqStatus);
    }

    public void ResetCampaign()
    {
        campaignContractPool.ResetCampaign();
    }
}
