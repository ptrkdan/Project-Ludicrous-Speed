using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue = 0;
    private readonly List<StatModifier> modifiers = new List<StatModifier>();
    private bool isDirty = true;
    private float currValue;
    private float maxValue = 30;        // TODO: This will vary on the stat

    public float GetBaseValue() => baseValue;
    public float GetCalcValue() {
        if (isDirty) {
            currValue = CalculateFinalValue();
            isDirty = false;
        }
        return currValue;
    }

    public float CalculateFinalValue() {
        float finalValue = baseValue;
        float sumPercentAdd = 0;
        modifiers.ForEach((mod) => {
            if (mod.Type == StatModType.Flat) {
                finalValue += mod.Value;
            } else if (mod.Type == StatModType.PercentAdd) {
                sumPercentAdd += mod.Value;
            } else if (mod.Type == StatModType.PercentMult) {
                finalValue *= 1 + mod.Value;
            }

            Debug.Log($"sumPercentAdd: {sumPercentAdd}");
            finalValue *= 1 + sumPercentAdd;
        });

        return (float) Math.Round(Mathf.Clamp(finalValue, 0, maxValue), 4);
    }

    public void AddModifier(StatModifier modifier) {
        modifiers.Add(modifier);
        modifiers.Sort(CompareStatModifierOrder);
        isDirty = true;
    }

    public void RemoveModifier(StatModifier modifier) {
        modifiers.Remove(modifier);
        isDirty = true;
    }


    private int CompareStatModifierOrder(StatModifier a, StatModifier b) {
        if (a.Type > b.Type) {
            return 1;
        } else if (a.Type < b.Type) {
            return -1;
        }

        return 0;    // a.Type == b.Type
    }

}
