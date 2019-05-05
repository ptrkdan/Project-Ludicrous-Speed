using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    [SerializeField] string playerName = "TEST PILOT";
    [SerializeField] int experiencePoints;
    [SerializeField] int playerLevel = 1;

    [Header("Career")]
    [SerializeField] string title = "Cannon Fodder";
    [SerializeField] [TextArea] string titleDescription = "You're as expendable as the tissue they hand out at stations.";
    [SerializeField] List<GameObject> perks;    // TODO: Make Perks GameObject


    public string PlayerName { get => playerName; set => playerName = value; }

    public int ExperiencePoints { get => experiencePoints; set => experiencePoints = value; }
    public void IncreaseExperiencePoints(int amount) {
        experiencePoints += amount;
        if (experiencePoints >= 100) {
            LevelUp();
        }
    }

    public int PlayerLevel { get => playerLevel; set => playerLevel = value; }
    private void LevelUp() {
        playerLevel += 1;
        experiencePoints -= 100; // TODO: Required points depend on level
    }

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
        return InventoryManager.instance.Inventory;
    }
    
    public void AddToInventory(Loot item) {
        InventoryManager.instance.AddToInventory(item);
    }
    public bool RemoveFromInventory(Loot item) {
        return InventoryManager.instance.RemoveFromInventory(item);
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
    
    public string Title { get => title; set => title = value; }
    public string TitleDescription { get => titleDescription; set => titleDescription = value; }
}
