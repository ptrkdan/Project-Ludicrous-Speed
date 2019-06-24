using System.Collections.Generic;
using UnityEngine;

public abstract class ContractPool : ScriptableObject
{
    [SerializeField] protected List<ContractConfig> allContracts;

    protected ContractPrereq playerStatus;
    protected List<ContractConfig> unlockedContracts = new List<ContractConfig>();
    protected bool isDirty;

    public virtual List<ContractConfig> GetContracts()
    {
        if (isDirty || unlockedContracts.Count == 0)
            UpdateUnlockedContractsList();
        return unlockedContracts;
    }

    public virtual void UpdatePrereqStatus(ContractPrereq playerStatus)
    {
        this.playerStatus = playerStatus;
        isDirty = true;
    }

    protected virtual void UpdateUnlockedContractsList()
    {
        isDirty = false;
    }
}
