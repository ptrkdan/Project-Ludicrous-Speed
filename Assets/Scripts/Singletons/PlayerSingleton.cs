using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    #region Singleton
    public static PlayerSingleton instance;
    private void Awake()
    {
        if (instance)
        {
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] string playerName = "TEST PILOT";
    [SerializeField] int experiencePoints;
    [SerializeField] int playerLevel = 1;

    [Header("Career")]
    [SerializeField] string title = "Cannon Fodder";
    [SerializeField] [TextArea] string titleDescription = "You're as expendable as the tissue they hand out at stations.";
    [SerializeField] List<GameObject> perks;    // TODO: Make Perks GameObject

    public delegate void OnPlayerLevelUp(int level);
    public OnPlayerLevelUp onPlayerLevelUpCallback;

    private void OnDestroy()
    {
        SavePlayer();
    }

    private void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer(PlayerData data)
    {
        if (data == null) return;

        playerName = data.playerName;
        playerLevel = data.playerLevel;
        experiencePoints = data.experiencePoints;
        SetPrereqStatus(new ContractPrereq(data.playerPrereqStatus));
    }

    #region Basics
    public string PlayerName { get => playerName; set => playerName = value; }

    public int ExperiencePoints { get => experiencePoints; set => experiencePoints = value; }
    public void IncreaseExperiencePoints(int amount) {
        experiencePoints += amount;
        if (experiencePoints >= 100) {
            LevelUp();
        }
        SavePlayer();
    }

    public int PlayerLevel { get => playerLevel; set => playerLevel = value; }
    private void LevelUp() {
        playerLevel += 1;
        experiencePoints -= 100; // TODO: Required points depend on level
        onPlayerLevelUpCallback?.Invoke(playerLevel);
    }
    #endregion

    #region InventoryMananger
    public int GetCredits() {
        return InventoryManager.instance.Credits;
    }

    public void AddToCredits(int amount) {
        InventoryManager.instance.AddToCredits(amount);
    }

    public bool DeductFromCredits(int amount) {
        return InventoryManager.instance.DeductFromCredits(amount);
    }

    public List<Loot> GetInventory() {
        return InventoryManager.instance.GetPlayerInventory();
    }
    
    public void AddToInventory(Loot item) {
        InventoryManager.instance.AddToPlayerInventory(item);
    }
    public bool RemoveFromInventory(Loot item) {
        return InventoryManager.instance.RemoveFromPlayerInventory(item);
    }
    #endregion

    #region EquipmentManager
    public Equipment GetEquipment(EquipmentSlot slot) {
        return EquipmentManager.instance.GetEquipment(slot);
    }
    #endregion

    public Stat GetStat(StatType type) {
        return StatsManager.instance.GetStat(type);
    }

    public ContractPrereq GetPrereqStatus()
    {
        return ContractManager.instance.GetPlayerPrereqStatus();
    }

    public void SetPrereqStatus(ContractPrereq contractPrereq)
    {
        ContractManager.instance.SetPlayerPrereqStatus(contractPrereq);
    }
    
    public string Title { get => title; set => title = value; }
    public string TitleDescription { get => titleDescription; set => titleDescription = value; }
}
