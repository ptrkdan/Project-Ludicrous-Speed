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
    [SerializeField] List<Loot> inventory;

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

    public List<Loot> Inventory { get => inventory; }
    public void AddToInventory(Loot item) {
        inventory.Add(item);
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
    public bool RemoveFromInventory(Loot item) {
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
