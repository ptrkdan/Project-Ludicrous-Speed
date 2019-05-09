using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat {
    [SerializeField] StatType type;
    [SerializeField] float baseValue;
    private float _baseValue;
    private float calcValue;
    private List<StatModifier> modifiers;
    private bool isDirty;

    public float GetBaseValue() => baseValue;
    public void SetBaseValue(float newValue) => baseValue = newValue;
    public float GetCalcValue() {
        if (isDirty || baseValue != _baseValue) calcValue = CalculateFinalValue();
        return calcValue;
    }

    public void UpdateCalcValue() {
        calcValue = CalculateFinalValue();
    }

    public float CalculateFinalValue() {
        float finalValue = baseValue;
        float sumPercentAdd = 0;
        modifiers.ForEach((mod) => {
            if (mod.ModType == StatModType.Flat) {
                finalValue += mod.Value;
            } else if (mod.ModType == StatModType.PercentAdd) {
                sumPercentAdd += mod.Value;
            } else if (mod.ModType == StatModType.PercentMult) {
                finalValue *= 1 + mod.Value;
            }

            finalValue *= 1 + sumPercentAdd;
        });

        _baseValue = baseValue;
        isDirty = false;
        return (float) Math.Round(finalValue, 4);
    }

    public Stat() {
        _baseValue = 0;
        modifiers = new List<StatModifier>();
        isDirty = true;
    }

    public Stat(StatType type) : this() {
        this.type = type;
        baseValue = 0;
        calcValue = 0;
    }

    public Stat(StatType type, float baseValue): this(type) {
        this.baseValue = baseValue;
        _baseValue = baseValue;
        calcValue = baseValue;
    }


    public void AddModifier(StatModifier modifier) {
        modifiers.Add(modifier);
        modifiers.Sort(CompareStatModifierOrder);
        isDirty = true;
    }

    public void RemoveModifier(object source) {
        int numRemoved = modifiers.RemoveAll(mod => mod.Source == source); // TODO: Check if it's really removing
        isDirty = true;
    }


    private int CompareStatModifierOrder(StatModifier a, StatModifier b) {
        if (a.ModType > b.ModType) {
            return 1;
        } else if (a.ModType < b.ModType) {
            return -1;
        }

        return 0;    // a.Type == b.Type
    }

}
