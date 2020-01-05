using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager instance;
    private void Awake()
    {
        if (instance)
        {
            return;
        }
        instance = this;

    }
    #endregion

    [SerializeField] private int credits;
    [SerializeField] private List<Loot> playerInventory;

    [Header("Market Inventory")]
    [SerializeField] private int numItems = 9;
    [SerializeField] private List<Loot> marketInventory;
    [SerializeField] private List<Loot> buybackInventory;

    [Header("[DEBUG]")]
    [SerializeField] private List<LootConfig> availableMarketItems;

    public delegate void OnPlayerInventoryChanged();
    public OnPlayerInventoryChanged onPlayerInventoryChangedCallback;

    public delegate void OnMarketInventoryChanged();
    public OnMarketInventoryChanged onMarketInvetoryChangedCallback;

    public delegate void OnBuybackInventoryChanged();
    public OnBuybackInventoryChanged onBuybackInventoryChangedCallback;


    private void SaveInventory()
    {
        SaveSystem.SaveInventoryData(this);
    }

    public void LoadInventory(InventoryData data)
    {
        if (data == null) return;

        credits = data.credits;

        for (int i = 0; i < data.playerInventory.Count; i++)
        {
            LootConfig config = ItemDict.GetItem(data.playerInventory[i]);
            Loot loot = config.Create();

            playerInventory.Add(loot);
            if (loot.GetLootType() == LootType.Equipment)
            {
                Equipment equipment = (Equipment)loot;
                equipment.SetStatModValueFromSave(data.playerInventoryStats[i]);
            }
        }
    }

    public int Credits { get => credits; private set => credits = value; }
    public void AddToCredits(int amount)
    {
        credits += amount;
        SaveInventory();
    }

    public bool DeductFromCredits(int amount)
    {
        bool didDeduct = false;
        if (credits >= amount)
        {
            credits -= amount;
            didDeduct = true;
            SaveInventory();
        }

        return didDeduct;
    }

    #region Player Inventory
    public List<Loot> GetPlayerInventory() => playerInventory;
    public void AddToPlayerInventory(Loot item)
    {
        playerInventory.Add(item);
        onPlayerInventoryChangedCallback?.Invoke();
        SaveInventory();
    }
    public bool RemoveFromPlayerInventory(Loot item)
    {
        bool didRemove = false;
        if (playerInventory.Remove(item))
        {
            onPlayerInventoryChangedCallback?.Invoke();
            didRemove = true;
            SaveInventory();
        }
        return didRemove;
    }
    #endregion

    #region Market Inventory
    public List<Loot> GetMarketInventory()
    {
        if (marketInventory == null || marketInventory.Count == 0)
        {
            RestockMarketInventory();
        }

        return marketInventory;
    }

    public void RestockMarketInventory()
    {
        if (availableMarketItems.Count == 0) return;
        marketInventory.Clear();
        for (int i = 0; i < numItems; i++)
        {
            Loot item = availableMarketItems[Random.Range(0, availableMarketItems.Count)].Create();
            marketInventory.Add(item);
        }
        onMarketInvetoryChangedCallback?.Invoke();
    }

    public bool RemoveFromMarketInventory(Loot item)
    {
        bool didRemove = false;
        if (marketInventory.Remove(item))
        {
            onMarketInvetoryChangedCallback?.Invoke();
            didRemove = true;
        }

        return didRemove;
    }

    public List<Loot> GetBuybackInventory() => buybackInventory;

    public void AddToBuybackInventory(Loot item)
    {
        buybackInventory.Add(item);
        onBuybackInventoryChangedCallback?.Invoke();
    }

    public bool RemoveFromBuybackInventory(Loot item)
    {
        bool didRemove = false;
        if (buybackInventory.Remove(item))
        {
            onBuybackInventoryChangedCallback?.Invoke();
            didRemove = true;
        }

        return didRemove;
    }

    #endregion
}
