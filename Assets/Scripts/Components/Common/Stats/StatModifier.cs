using UnityEngine;

[System.Serializable]
public class StatModifier
{
    public readonly float Value;
    public readonly StatModType Type;
    public readonly float Duration;

    public StatModifier(float value, StatModType type, float duration = 0) {
        Value = value;
        Type = type;
        Duration = duration;
    }
}

public enum StatModType { Flat, PercentAdd, PercentMult }
