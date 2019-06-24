using System;
using UnityEngine;

[Serializable]
public class ContractPrereq
{
    [SerializeField] int[] criteria = new int[Enum.GetValues(typeof(PrereqType)).Length];

    public ContractPrereq()
    {
        for (int i = 0; i < Enum.GetValues(typeof(PrereqType)).Length; i++)
        {
            criteria[i] = -1;
        }
    }

    public ContractPrereq(
        int playerLevel, int careerLevel, int reputationLevel, int shipPowerLevel, int campaignLevel)
    {
        criteria[(int)PrereqType.PlayerLevel] = playerLevel;
        criteria[(int)PrereqType.CareerLevel] = careerLevel;
        criteria[(int)PrereqType.ReputationLevel] = reputationLevel;
        criteria[(int)PrereqType.ShipPowerLevel] = shipPowerLevel;
        criteria[(int)PrereqType.CompletedCampaignLevel] = campaignLevel;
    }

    public bool Check(ContractPrereq playerStatus)          // OPTIM: Check only updated criteria
    {
        bool pass = true;

        for (int i = 0; i < Enum.GetValues(typeof(PrereqType)).Length; i++)
        {
            if (playerStatus.criteria[i] < criteria[i])
            {
                return false;
            }
        }

        return pass;
    }
}
