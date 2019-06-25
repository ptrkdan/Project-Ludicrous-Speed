using System.Collections.Generic;
using UnityEngine;

public abstract class ContractGate : ScriptableObject
{
    public abstract List<ContractConfig> GetContracts();
    public abstract void UpdatePlayerPrereqStatus(ContractPrereq playerPrereqStatus);
    public abstract void ResetAllContractFlags();
}
