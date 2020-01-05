using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Contract Config")]
public class ContractConfig : ScriptableObject
{
    [Header("Basic Details")]
    [SerializeField] private int contractID = 0;
    [SerializeField] private string contractTitle = "New contract";
    [SerializeField] private ContractType contractType = ContractType.Standard;
    [SerializeField, TextArea] private string contractDetails = "So here's the story...";
    [SerializeField] private int runDistance = 1000;
    [SerializeField] private AssociatedFaction associatedFaction = AssociatedFaction.None;
    [SerializeField] private ContractPrereq unlockPrereq = new ContractPrereq();

    [Header("Non-Friendlies Parameters")]
    [SerializeField, Range(1, 10)] private int difficultyLevel = 1;
    [Space]
    [SerializeField, Range(1, 10)] private int securityLevel = 1;
    [SerializeField] private List<SecurityUnitController> securityUnits;               // TODO Change to SecurityController
    [SerializeField] private bool hasSecurityBoss = false;
    [SerializeField] private List<EnemyController> potentialSecurityBoss;       // TODO Change to SecurityBossController
    [Space]
    [SerializeField, Range(1, 10)] private int creatureLevel = 1;
    [SerializeField] private List<CreatureController> creatures;
    [SerializeField] private bool hasCreatureBoss = false;
    [SerializeField] private List<EnemyController> potentialCreatureBoss;       // TODO Change to CreatureBossController
    [Space]
    [SerializeField, Range(1, 10)] private int debrisLevel = 1;
    [SerializeField] private List<DebrisController> debris;

    [Header("Rewards")]
    [SerializeField, Range(1, 10)] private int lootLevel = 1;
    [SerializeField, Range(1, 10)] private int creditRewardLevel = 1;
    [SerializeField] private List<LootFactory> lootDrops;
    [SerializeField] private List<LootConfig> specialLootDrops;

    [Header("Misc.")]
    [SerializeField] private List<PickUpLootConfig> pickUps;
    [SerializeField] private List<float> pickUpDropRates;           // TODO Refactor

    private ContractFlags flags;

    public int ContractID { get => contractID; set => contractID = value; }
    public ContractType ContractType { get => contractType; set => contractType = value; }
    public string ContractTitle { get => contractTitle; set => contractTitle = value; }
    public string ContractDetails { get => contractDetails; set => contractDetails = value; }
    public int RunDistance { get => runDistance; set => runDistance = value; }
    public AssociatedFaction AssociatedFaction { get => associatedFaction; set => associatedFaction = value; }
    public ContractPrereq UnlockPrereq { get => unlockPrereq; set => unlockPrereq = value; }
    public int DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }
    public int SecurityLevel { get => securityLevel; set => securityLevel = value; }
    public List<SecurityUnitController> SecurityUnits { get => securityUnits; set => securityUnits = value; }
    public List<EnemyController> PotentialSecurityBoss { get => potentialSecurityBoss; set => potentialSecurityBoss = value; }
    public int CreatureLevel { get => creatureLevel; set => creatureLevel = value; }
    public List<CreatureController> Creatures { get => creatures; set => creatures = value; }
    public List<EnemyController> PotentialCreatureBoss { get => potentialCreatureBoss; set => potentialCreatureBoss = value; }
    public int DebrisLevel { get => debrisLevel; set => debrisLevel = value; }
    public List<DebrisController> Debris { get => debris; set => debris = value; }
    public int LootLevel { get => lootLevel; set => lootLevel = value; }
    public int CreditRewardLevel { get => creditRewardLevel; set => creditRewardLevel = value; }
    public List<LootFactory> LootDrops { get => lootDrops; set => lootDrops = value; }
    public List<LootConfig> SpecialLootDrops { get => specialLootDrops; set => specialLootDrops = value; }
    public List<PickUpLootConfig> PickUps { get => pickUps; set => pickUps = value; }
    public List<float> PickUpDropRates { get => pickUpDropRates; set => pickUpDropRates = value; }
    public ContractFlags Flags { get => flags; set => flags = value; }
}

public enum ContractType { Campaign, Standard, Special }
public enum PrereqType
{
    PlayerLevel,
    SmugglerReputationLevel,
    FactionAReputationLevel,
    FactionBReputationLevel,
    FactionCReputationLevel,
    ShipPowerLevel,
    CompletedCampaignLevel
}
public enum AssociatedFaction { None, Smugglers, FactionA, FactionB, FactionC }

[Flags]
public enum ContractFlags { None, isUnlocked, isCompleted };