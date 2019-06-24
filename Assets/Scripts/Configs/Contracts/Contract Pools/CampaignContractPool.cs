using UnityEngine;

[CreateAssetMenu(menuName = "Pools/Campaign Contract Pool")]
public class CampaignContractPool : ContractPool
{
    protected override void UpdateUnlockedContractsList()
    {
        if (playerStatus == null)
        {
            playerStatus = new ContractPrereq();
        }

        // Remove campaign contracts that have already been completed
        for (int i = unlockedContracts.Count - 1; i >= 0; i--)
        {
            if (!unlockedContracts[i].UnlockPrereq.Check(playerStatus)
                || unlockedContracts[i].Flags.HasFlag(ContractFlags.isCompleted))
            {
                unlockedContracts[i].Flags ^= ContractFlags.isUnlocked;
                unlockedContracts.Remove(unlockedContracts[i]);
            }

        }

        foreach (ContractConfig contract in allContracts)
        {
            if (!contract.Flags.HasFlag(ContractFlags.isUnlocked)
                && !contract.Flags.HasFlag(ContractFlags.isCompleted)
                && contract.UnlockPrereq.Check(playerStatus))
            {
                contract.Flags |= ContractFlags.isUnlocked;
                unlockedContracts.Add(contract);
            }
        }

        base.UpdateUnlockedContractsList();
    }

    public void ResetCampaign()
    {
        foreach (ContractConfig contract in allContracts)
        {
            contract.Flags &= ~ContractFlags.isUnlocked;
            contract.Flags &= ~ContractFlags.isCompleted;
        }

        unlockedContracts.Clear();
    }

}
