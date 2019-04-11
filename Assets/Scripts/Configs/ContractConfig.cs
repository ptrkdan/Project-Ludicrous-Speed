using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Contract Config")]
public class ContractConfig : ScriptableObject {
    [SerializeField] string contractTitle;
    [SerializeField] [TextArea] string contractDetails;
    [SerializeField] int runDistance = 100;
    [SerializeField] List<PickUpLootConfig> availablePickUps;
    [SerializeField] List<LootConfig> contractRewards;

    [Header("Run Parameters")]
    [SerializeField] [Range(1, 10)] int contractLootLevel = 1;
    [SerializeField] [Range(1, 10)] int contractDifficultyLevel = 1;
    [SerializeField] [Range(0, 1f)] float pickupDropRate = 1;


    public string GetContractTitle() { return contractTitle; }
    public string GetContractDetails() { return contractDetails; }
    public int GetRunDistance() { return runDistance; }
    public List<LootConfig> GetContractRewards() { return contractRewards; }
    public int GetContractLootLevel() { return contractLootLevel; }
    public int GetContractDifficultyLevel() { return contractDifficultyLevel; }
    public float GetPickupDropRate() { return pickupDropRate; }

}
