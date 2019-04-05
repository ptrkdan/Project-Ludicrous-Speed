using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue = 0;
    private float totalModifier = 0;

    public float GetBaseValue() => baseValue;
    public float GetCalculatedValue() => baseValue + totalModifier;

    public void AddModifier(float modifier) {
        totalModifier += modifier;
    }

    public void RemoveModifier(float modifier) {
        totalModifier -= modifier;
    }

    public void MultiplyModifier(float modifier) {
        baseValue *= modifier;
    }

    public void DivideModifier(float modifier) {
        baseValue /= modifier;
    }
}
