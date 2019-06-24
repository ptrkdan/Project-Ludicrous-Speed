using System.Collections.Generic;
using UnityEngine;

public abstract class ContractGate : ScriptableObject
{
    protected ContractPrereq prereqs;

    public abstract List<ContractConfig> GetContracts();
    public abstract void UpdatePrereqs(ContractPrereq playerStatus);
}
