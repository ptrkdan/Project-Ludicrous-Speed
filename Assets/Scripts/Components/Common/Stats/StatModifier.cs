using System;

[Serializable]
public class StatModifier
{
    public object Source;
    public readonly StatType StatType;
    public readonly StatModType ModType;
    public float Value;
    public float Duration;

    public StatModifier(object source, StatType statType)
    {
        StatType = statType;
        ModType = StatModType.Flat;
        Value = 0;
        Source = source;
        Duration = 0;
    }

    public StatModifier(object source, StatType statType, StatModType modType, float value) : this(source, statType)
    {
        ModType = modType;
        Value = value;
    }

    public StatModifier(object source, StatType statType, StatModType modType, float value, float duration) : this(source, statType, modType, value)
    {
        Duration = duration;
    }
}

public enum StatModType { Flat, PercentAdd, PercentMult }
