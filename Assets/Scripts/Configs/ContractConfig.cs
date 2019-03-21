using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Contract Config")]
public class ContractConfig : ScriptableObject {
    [SerializeField] string contractTitle;
    [SerializeField] [TextArea] string contractDetails;
    [SerializeField] int contractLootLevel = 1;
    [SerializeField] int contractDifficultyLevel = 1;
    [SerializeField] int runDistance = 1000;
    [SerializeField] List<Loot> contractRewards;

    public string GetContractTitle() { return contractTitle; }
    public string GetContractDetails() { return contractDetails; }
    public int GetContractLootLevel() { return contractLootLevel; }
    public int GetContractDifficultyLevel() { return contractDifficultyLevel; }
    public int GetRunDistance() { return runDistance; }
    public List<Loot> GetContractRewards() { return contractRewards; }

}
