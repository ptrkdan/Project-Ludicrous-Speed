using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    [SerializeField] string playerName = "TEST PILOT";
    [SerializeField] int experiencePoints;
    [SerializeField] int playerLevel = 1;

    [SerializeField] InventoryManager inventory;
    [SerializeField] EquipmentManager equipments;

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

    public int GetCredits() {
        return InventoryManager.instance.Credits;
    }

    public void AddToCredits(int amount) {
        InventoryManager.instance.AddToCredits(amount);
    }

    public bool DeductFromCredits(int amount) {
        return InventoryManager.instance.DeductFromCredits(amount);
    }

    public List<LootConfig> GetInventory() {
        return InventoryManager.instance.Inventory;
    }
    
    public void AddToInventory(LootConfig item) {
        InventoryManager.instance.AddToInventory(item);
    }
    public bool RemoveFromInventory(LootConfig item) {
        return InventoryManager.instance.RemoveFromInventory(item);
    }


    public EquipmentConfig GetEquipment(EquipmentSlot slot) {
        return EquipmentManager.instance.GetEquipment(slot);
    }

    public string Title { get => title; set => title = value; }
    public string TitleDescription { get => titleDescription; set => titleDescription = value; }
}
