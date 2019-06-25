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
    [SerializeField] int careerLvl;
    [SerializeField] int reputationLvl;
    [SerializeField] int shipPowerLvl;
    [SerializeField] int campaign;

    ContractPrereq playerPrereqStatus;

    private void Start()
    {
        playerPrereqStatus = new ContractPrereq(
            playerLvl, careerLvl, reputationLvl, shipPowerLvl, campaign);

        PlayerSingleton.instance.onPlayerLevelUpCallback += OnPlayerLevelUp;
        PlayerSingleton.instance.onCareerLevelUpCallback += OnCareerLevelUp;
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    public MasterContractGate GetMasterContractGate() => masterContractGate;

    private void OnPlayerLevelUp(int level)
    {
        playerPrereqStatus.Update(PrereqType.PlayerLevel, level);
        UpdatePlayerPrereqStatus();
    }

    private void OnCareerLevelUp(int level)
    {
        playerPrereqStatus.Update(PrereqType.CareerLevel, level);
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

    private void OnCampaignMissionComplete(int campaignNum)
    {
        playerPrereqStatus.Update(PrereqType.CompletedCampaignLevel, campaignNum);
        UpdatePlayerPrereqStatus();
    }

    private void UpdatePlayerPrereqStatus()
    {
        Debug.Log("Updating player prereq status");
        masterContractGate.UpdatePlayerPrereqStatus(playerPrereqStatus);
    }
}
