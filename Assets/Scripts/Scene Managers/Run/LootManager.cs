using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] List<PickUpLootConfig> pickUpLootList = new List<PickUpLootConfig>();

    public PickUpLootConfig DropLoot() {

        PickUpLootConfig loot = pickUpLootList[Random.Range(0, pickUpLootList.Count - 1)];
        if (Random.Range(0f, 1f) >= 1 - loot.Rarity) {
            return loot;
        } 
        return null;
    }
}
