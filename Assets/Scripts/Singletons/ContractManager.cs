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

    [SerializeField] private MasterContractGate masterContractGate;

    [Header("DEBUG")]
    [SerializeField] private int playerLvl;
    [SerializeField] private int smugglerReputationLvl;
    [SerializeField] private int FactionAReputationLvl;
    [SerializeField] private int FactionBReputationLvl;
    [SerializeField] private int FactionCReputationLvl;
    [SerializeField] private int shipPowerLvl;
    [SerializeField] private int campaign;
    [SerializeField] private bool resetCampaign;
    [SerializeField] private bool resetAllContractFlags;

    private ContractPrereq playerPrereqStatus;

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
            ResetCampaign();
            resetCampaign = false;
        }
        if (resetAllContractFlags)
        {
            ResetAllContractFlags();
            resetAllContractFlags = false;
        }
    }

    public MasterContractGate GetMasterContractGate() => masterContractGate;

    public ContractPrereq GetPlayerPrereqStatus() => playerPrereqStatus;
    public void SetPlayerPrereqStatus(ContractPrereq contractPrereq)
    {
        playerPrereqStatus = contractPrereq;
    }

    public void ResetCampaign()
    {
        masterContractGate.ResetCampaign();
    }

    public void ResetAllContractFlags()
    {
        masterContractGate.ResetAllContractFlags();
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
