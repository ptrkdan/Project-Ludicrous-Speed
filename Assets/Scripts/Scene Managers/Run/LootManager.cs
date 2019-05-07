﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    #region Singleton
    public static LootManager instance;

    private void Awake() {
        if (instance != null) {
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] List<PickUpLootConfig> availableLoot;
    [SerializeField] List<float> availableLootDropRate;

    public void ConfigureAvailableLoot(List<PickUpLootConfig> availableLoot, List<float> availableLootDropRate) 
    {
        this.availableLoot = availableLoot;
        this.availableLootDropRate = availableLootDropRate;
    }

    public PickUpLootConfig DropLoot()
    {
        List<PickUpLootConfig> droppedLoot = new List<PickUpLootConfig>();

        // Check if each possible loot drops
        for (int i = 0; i < availableLoot.Count; i++)
        {
            float dropRoll = Random.Range(0f, 100f);
            if (dropRoll >= 100f - availableLootDropRate[i])
            {
                droppedLoot.Add(availableLoot[i]);
            }
        }

        // Select one "dropped" loot
        if (droppedLoot.Count >= 1)
        {
            return droppedLoot[Random.Range(0, droppedLoot.Count)];
        }
        return null;
    }
}
