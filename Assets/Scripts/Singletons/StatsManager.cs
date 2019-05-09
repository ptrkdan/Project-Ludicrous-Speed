using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    #region Singleton
    public static StatsManager instance;
    private void Awake() {
        if (instance) {
            return;
        }
        instance = this;
    }
    #endregion


    [Header("Base Stats")]
    [SerializeField] protected int maxHealth = 1000;
    [SerializeField] protected Stat hull = new Stat(StatType.Hull);
    [SerializeField] protected Stat shield = new Stat(StatType.Shield);
    [SerializeField] protected Stat engine = new Stat(StatType.Engine);
    [SerializeField] protected Stat weapon = new Stat(StatType.Weapon);
    [SerializeField] protected Stat aux = new Stat(StatType.Aux);

    public int GetMaxHealth() => maxHealth;
    public Stat GetStat(StatType type)
    {
        Stat stat;
        switch (type)
        {
            case (StatType.Hull):
                stat = hull;
                break;
            case (StatType.Shield):
                stat = shield;
                break;
            case (StatType.Engine):
                stat = engine;
                break;
            case (StatType.Weapon):
                stat = weapon;
                break;
            case (StatType.Aux):
                stat = aux;
                break;
            default:
                stat = null;
                break;
        }

        return stat;
    }

    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Equipment newEquip, Equipment oldEquip) {
        if (newEquip != null) {
            foreach (StatType type in Enum.GetValues(typeof(StatType))) {
                if (type != StatType.None) {
                    StatModifier newMod = new StatModifier(
                        newEquip, type, StatModType.Flat, newEquip.GetStatModValue(type));
                    GetStat(type).AddModifier(newMod);
                } 
            }
        }

        if (oldEquip != null) {
            foreach (StatType type in Enum.GetValues(typeof(StatType))) {
                if (type != StatType.None) {
                    GetStat(type).RemoveModifier(oldEquip);
                }
            }
        }
    }
}
