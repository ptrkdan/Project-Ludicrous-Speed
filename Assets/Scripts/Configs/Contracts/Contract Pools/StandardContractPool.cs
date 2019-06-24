using UnityEngine;

[CreateAssetMenu(menuName = "Pools/Standard Contract Pool")]
public class StandardContractPool : ContractPool
{
    protected override void UpdateUnlockedContractsList()
    {
        if (playerStatus == null)
        {
            playerStatus = new ContractPrereq();
        }

        for (int i = unlockedContracts.Count - 1; i >= 0; i--)
        {
            if (!unlockedContracts[i].UnlockPrereq.Check(playerStatus))
            {
                unlockedContracts[i].Flags ^= ContractFlags.isUnlocked;
                unlockedContracts.Remove(unlockedContracts[i]);
            }
        }

        foreach (ContractConfig contract in allContracts)
        {
            if (!contract.Flags.HasFlag(ContractFlags.isUnlocked)
                && contract.UnlockPrereq.Check(playerStatus))
            {
                contract.Flags |= ContractFlags.isUnlocked;
                unlockedContracts.Add(contract);
            }
        }

        base.UpdateUnlockedContractsList();
    }
}
