using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Contract Config")]
public class ContractConfig : ScriptableObject {
    [SerializeField] string contractTitle;
    [SerializeField] [TextArea] string contractDetails;
    [SerializeField] int runDistance = 100;
    [SerializeField] List<LootConfig> contractRewards;

    [Header("Run Parameters")]
    [SerializeField] int contractLootLevel = 1;
    [SerializeField] int contractDifficultyLevel = 1;

    public string GetContractTitle() { return contractTitle; }
    public string GetContractDetails() { return contractDetails; }
    public int GetRunDistance() { return runDistance; }
    public List<LootConfig> GetContractRewards() { return contractRewards; }
    public int GetContractLootLevel() { return contractLootLevel; }
    public int GetContractDifficultyLevel() { return contractDifficultyLevel; }

}
