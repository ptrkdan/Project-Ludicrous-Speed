using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStats : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected float currentHealth;

    [Header("Base Stats")]
    [SerializeField] protected Stat hull;
    [SerializeField] protected Stat shield;
    [SerializeField] protected Stat engine;
    [SerializeField] protected Stat weapon;
    [SerializeField] protected Stat aux;

    private List<StatModifier> modifiers = new List<StatModifier>();

    public delegate void OnStatChange(StatType type);
    public OnStatChange onStatChange;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    #region Methods: Unity

    private void OnDisable()
    {
        foreach (StatModifier modifier in modifiers)
        {
            RemoveBuff(modifier.StatType, modifier);
        }
    }

    #endregion Methods: Unity

    #region Methods: Stats

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

    public void UpdateStats()
    {
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            GetStat(type).UpdateCalcValue();
        }
    }

    #endregion Methods: Stats

    #region Methods: Damage

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= CalculateDamage(damage);
    }

    public virtual void RepairDamage(float repair)
    {
        currentHealth = Mathf.Clamp(currentHealth + repair, 0, maxHealth);
    }

    protected virtual float CalculateDamage(float damage)
    {
        float finalDamage = damage - GetStat(StatType.Hull).Value;      // TODO: Write formula for damage mitigation
        return Mathf.Clamp(finalDamage, 0, float.MaxValue);
    }

    #endregion Methods: Damage

    #region Methods: Buff

    public virtual void SetBuff(StatType type, StatModifier modifier)
    {
        if (modifier.Duration > 0)
        {
            StartCoroutine(SetBuffForDuration(type, modifier));
            modifiers.Add(modifier);
        }
        else
        {
            Debug.Log($"Adding buff({modifier.Value}) to {type}");
            GetStat(type).AddModifier(modifier);
        }
        onStatChange?.Invoke(type);
    }

    public virtual void RemoveBuff(StatType type, StatModifier modifier)
    {
        GetStat(type).RemoveModifier(modifier.Source);
        onStatChange?.Invoke(type);
    }

    private IEnumerator SetBuffForDuration(StatType type, StatModifier modifier)
    {
        GetStat(type).AddModifier(modifier);
        yield return new WaitForSeconds(modifier.Duration);
        modifiers.Remove(modifier);
        RemoveBuff(type, modifier);
    }

    #endregion Methods: Buff

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}

