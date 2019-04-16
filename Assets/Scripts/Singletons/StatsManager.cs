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
    PlayerStats stats;

    private void Start() {
        stats = GetComponent<PlayerStats>();
    }

    public Stat GetStat(StatType type) {
        return stats.GetStat(type);
    }
}
