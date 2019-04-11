using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager instance;
    private void Awake() {
        if (instance) {
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] int credits;
    [SerializeField] List<LootConfig> inventory;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int Credits { get => credits; set => credits = value; }
    public void AddToCredits(int amount) { credits += amount; }
    public bool DeductFromCredits(int amount) {
        bool didDeduct = false;
        if (credits >= amount) {
            credits -= amount;
            didDeduct = true;
        }

        return didDeduct;
    }

    public List<LootConfig> Inventory { get => inventory; }
    public void AddToInventory(LootConfig item) {
        inventory.Add(item);
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
    public bool RemoveFromInventory(LootConfig item) {
        bool didRemove = false;
        if (inventory.Remove(item)) {
            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
            didRemove = true;
        }
        return didRemove;
    }
}
