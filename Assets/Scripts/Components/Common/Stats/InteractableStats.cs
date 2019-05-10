using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStats : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    [Header("Base Stats")]
    [SerializeField] protected Stat hull;
    [SerializeField] protected Stat shield;
    [SerializeField] protected Stat engine;
    [SerializeField] protected Stat weapon;
    [SerializeField] protected Stat aux;

    List<StatModifier> modifiers = new List<StatModifier>();

    public delegate void OnStatChange(StatType type);
    public OnStatChange onStatChange;

    public int GetCurrentHealth() => currentHealth;
    public void SetCurrentHealth(int currentHealth) => this.currentHealth = currentHealth;
    public Stat GetStat(StatType type) {
        Stat stat;
        switch(type)
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

    private void OnDisable()
    { 
        foreach(StatModifier modifier in modifiers)
        {
            RemoveBuff(modifier.StatType, modifier);
        }
    }

    protected virtual int CalculateDamage(int damage) {
        int finalDamage = damage - Mathf.FloorToInt(GetStat(StatType.Hull).GetCalcValue());
        return Mathf.Clamp(finalDamage, 0, int.MaxValue);
    }

    public virtual void TakeDamage(int damage) {
            currentHealth -= CalculateDamage(damage);
    }

    public virtual void RepairDamage(int repair) {
        currentHealth = Mathf.Clamp(currentHealth + repair, 0, maxHealth);
    }

    public virtual void SetBuff(StatType type, StatModifier modifier) {
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

    public virtual void RemoveBuff(StatType type, StatModifier modifier) {
        GetStat(type).RemoveModifier(modifier.Source);
        onStatChange?.Invoke(type);
    }

    IEnumerator SetBuffForDuration(StatType type, StatModifier modifier) {
        GetStat(type).AddModifier(modifier);
        yield return new WaitForSeconds(modifier.Duration);
        modifiers.Remove(modifier);
        RemoveBuff(type, modifier);     
    }

    public void UpdateStats() {
        foreach(StatType type in Enum.GetValues(typeof(StatType))) {
            GetStat(type).UpdateCalcValue();
        }
    }

    public virtual void Die() { 
        Destroy(gameObject);
    }
}

