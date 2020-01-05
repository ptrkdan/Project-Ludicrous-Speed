using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] public float baseValue = 0;
    [SerializeField] protected StatType type;

    protected float initialValue; // For initialization without baseValue assigned
    protected float calcValue;
    protected List<StatModifier> modifiers;
    protected bool isDirty;

    public float Value {
        get {
            if (isDirty || baseValue != initialValue)
            {
                calcValue = CalculateFinalValue();
            }

            return calcValue;
        }
    }

    #region Constructors

    public Stat()
    {
        initialValue = 0;
        modifiers = new List<StatModifier>();
        isDirty = true;
    }

    public Stat(StatType type) : this()
    {
        this.type = type;
        calcValue = 0;
    }

    public Stat(StatType type, float baseValue) : this(type)
    {
        this.baseValue = baseValue;
        initialValue = baseValue;
        calcValue = baseValue;
    }

    #endregion Constructors

    #region Methods: Value Calculation

    public void UpdateCalcValue()
    {
        calcValue = CalculateFinalValue();
    }

    private float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;
        modifiers.ForEach((mod) =>
        {
            CalculateModValue(mod, ref finalValue, ref sumPercentAdd);

            finalValue *= 1 + sumPercentAdd;
        });

        initialValue = baseValue;
        isDirty = false;
        return (float)Math.Round(finalValue, 4);
    }

    private void CalculateModValue(StatModifier mod, ref float finalValue, ref float sumPercentAdd)
    {
        if (mod.ModType == StatModType.Flat)
        {
            finalValue += mod.Value;
        }
        else if (mod.ModType == StatModType.PercentAdd)
        {
            sumPercentAdd += mod.Value;
        }
        else if (mod.ModType == StatModType.PercentMult)
        {
            finalValue *= 1 + mod.Value;
        }
    }

    #endregion Methods: Value Calculation

    #region Methods: Modifier

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        modifiers.Sort(CompareStatModifierOrder);
        isDirty = true;
    }

    public void RemoveModifier(object source)
    {
        int numRemoved = modifiers.RemoveAll(mod => mod.Source == source); // TODO: Check if it's really removing
        isDirty = true;
    }

    private int CompareStatModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.ModType > b.ModType)
        {
            return 1;
        }
        else if (a.ModType < b.ModType)
        {
            return -1;
        }

        return 0;    // a.Type == b.Type
    }

    #endregion Methods: Modifier
}

public enum StatType { Hull, Shield, Engine, Weapon, Aux }