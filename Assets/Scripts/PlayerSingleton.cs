using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    [SerializeField] string playerName = "TEST PILOT";
    [SerializeField] int experiencePoints;
    [SerializeField] int playerLevel = 1;

    [SerializeField] int credits;
    [SerializeField] List<LootConfig> inventory;

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

    public int Credits { get => credits; set => credits = value; }
    public void AddToCredits(int amount) { credits += amount; }
    public void DeductFromCredits(int amount) { credits -= amount; }

    public List<LootConfig> Inventory { get => inventory; set => inventory = value; }
    public void AddToInventory(LootConfig item) { inventory.Add(item); }
    public void RemoveFromInventory(LootConfig item) {
        // TODO: Implement
    }
}
