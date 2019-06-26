using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    #region Singleton
    public static ContractManager instance;
    private void Awake()
    {
        if (instance)
        {
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] MasterContractGate masterContractGate;

    [Header("DEBUG")]
    [SerializeField] int playerLvl;
    [SerializeField] int smugglerReputationLvl;
    [SerializeField] int FactionAReputationLvl;
    [SerializeField] int FactionBReputationLvl;
    [SerializeField] int FactionCReputationLvl;
    [SerializeField] int shipPowerLvl;
    [SerializeField] int campaign;

    [SerializeField] bool resetCampaign;
    [SerializeField] bool resetAllContractFlags;

    ContractPrereq playerPrereqStatus;

    private void Start()
    {
        if (playerPrereqStatus == null)
        {
            playerPrereqStatus = new ContractPrereq(
            playerLvl, smugglerReputationLvl, FactionAReputationLvl,
            FactionBReputationLvl, FactionCReputationLvl, shipPowerLvl, campaign);
        }

        masterContractGate.UpdatePlayerPrereqStatus(playerPrereqStatus);

        PlayerSingleton.instance.onPlayerLevelUpCallback += OnPlayerLevelUp;
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        GameSession.instance.OnCampaignMissionCompleteCallback += OnCampaignMissionComplete;
    }

    private void Update()
    {
        if (resetCampaign)
        {
            masterContractGate.ResetCampaign();
            resetCampaign = false;
        }
        if (resetAllContractFlags)
        {
            masterContractGate.ResetAllContractFlags();
            resetAllContractFlags = false;
        }
    }

    public MasterContractGate GetMasterContractGate() => masterContractGate;

    public ContractPrereq GetPlayerPrereqStatus() => playerPrereqStatus;
    public void SetPlayerPrereqStatus(ContractPrereq contractPrereq)
    {
        playerPrereqStatus = contractPrereq;
    }
    
    private void OnPlayerLevelUp(int level)
    {
        playerPrereqStatus.Update(PrereqType.PlayerLevel, level);
        UpdatePlayerPrereqStatus();
    }

    private void OnReputationLevelUp(GameObject faction, int level)
    {
        // TODO: Implement faction condition
        UpdatePlayerPrereqStatus();
    }

    private void OnEquipmentChanged(Equipment newEquip, Equipment oldEquip)
    {
        // TODO: Implement ship power
        UpdatePlayerPrereqStatus();
    }

    private void OnCampaignMissionComplete(bool success, int campaignID)
    {
        if (success)
        {
            playerPrereqStatus.Update(PrereqType.CompletedCampaignLevel, campaignID + 1);
            UpdatePlayerPrereqStatus();
        }
    }

    private void UpdatePlayerPrereqStatus()
    {
        Debug.Log("Updating player prereq status");
        masterContractGate.UpdatePlayerPrereqStatus(playerPrereqStatus);
    }
}
