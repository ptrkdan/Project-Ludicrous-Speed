using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gates/Standard Contract Gate")]
public class StandardContractGate : ContractGate
{
    [SerializeField] private List<ContractPool> standardContractPools;

    private List<ContractConfig> unlockedStandardContracts = new List<ContractConfig>();
    private bool isDirty = true;

    public override List<ContractConfig> GetContracts()
    {
        if (isDirty || unlockedStandardContracts.Count == 0)
            UpdateUnlockedStandardContractsList();
        return unlockedStandardContracts;
    }


    public override void UpdatePlayerPrereqStatus(ContractPrereq playerPrereqStatus)
    {
        foreach (ContractPool pool in standardContractPools)
        {
            pool.UpdatePrereqStatus(playerPrereqStatus);
        }

        isDirty = true;
    }

    public override void ResetAllContractFlags()
    {
        foreach (ContractPool pool in standardContractPools)
        {
            pool.ResetFlags();
        }
    }

    private void UpdateUnlockedStandardContractsList()
    {
        unlockedStandardContracts.Clear();
        foreach (ContractPool pool in standardContractPools)
        {
            unlockedStandardContracts.AddRange(pool.GetContracts());
        }

        isDirty = false;
    }
}
