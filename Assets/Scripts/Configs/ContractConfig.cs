using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Contract Config")]
public class ContractConfig : ScriptableObject {
    [SerializeField] string contractTitle;
    [SerializeField] [TextArea] string contractDetails;
    [SerializeField] int runDistance = 100;
    [SerializeField] List<PickUpLootConfig> availablePickUps;
    [SerializeField] List<float> availablePickUpDropRates;
    [SerializeField] List<LootConfig> contractRewards;
    [SerializeField] List<float> contractRewardDropRates;

    [Header("Run Parameters")]
    [SerializeField] [Range(1, 10)] int contractLootLevel = 1;
    [SerializeField] [Range(1, 10)] int contractDifficultyLevel = 1;


    public string GetContractTitle() => contractTitle;
    public string GetContractDetails() => contractDetails;
    public int GetRunDistance() => runDistance;
    public List<PickUpLootConfig> GetAvailablePickUps() => availablePickUps;
    public List<float> GetAvailablePickUpDropRates() => availablePickUpDropRates;
    public List<LootConfig> GetContractRewards() => contractRewards;
    public List<float> GetContractRewardDropRates() => contractRewardDropRates;
    public int GetContractLootLevel() => contractLootLevel;
    public int GetContractDifficultyLevel() => contractDifficultyLevel;

}