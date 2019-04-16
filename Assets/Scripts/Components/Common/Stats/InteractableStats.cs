using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStats : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    [Header("Base Stats")]
    [SerializeField] float hull = 0;
    [SerializeField] float shield = 0;
    [SerializeField] float engine = 0;
    [SerializeField] float weapon = 0;
    [SerializeField] float aux = 0;

    protected Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
    
    private void Awake() {
        currentHealth = maxHealth;
        stats[StatType.Hull] = new Stat(StatType.Hull, hull);
        stats[StatType.Shield] = new Stat(StatType.Shield, shield);
        stats[StatType.Engine] = new Stat(StatType.Engine, engine);
        stats[StatType.Weapon] = new Stat(StatType.Weapon, weapon);
        stats[StatType.Aux] = new Stat(StatType.Aux, aux);
    }

    public int GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(int currentHealth) { this.currentHealth = currentHealth; }
    public Stat GetStat(StatType type) { return stats[type]; }

    protected virtual int CalculateDamage(int damage) {
        int finalDamage = damage - Mathf.FloorToInt(stats[StatType.Hull].GetCalcValue());
        return Mathf.Clamp(finalDamage, 0, int.MaxValue);
    }

    public virtual void TakeDamage(int damage) {
            currentHealth -= CalculateDamage(damage);
    }

    public virtual void RepairDamage(int repair) {
        currentHealth = Mathf.Clamp(currentHealth + repair, 0, maxHealth);
    }

    public void SetBuff(StatType type, StatModifier modifier) {
        StartCoroutine(SetBuffForDuration(type, modifier));
    }

    public void RemoveBuff(StatType type, StatModifier modifier) {
        stats[type].RemoveModifier(modifier.Source);
    }

    IEnumerator SetBuffForDuration(StatType type, StatModifier modifier) {
        stats[type].AddModifier(modifier);
        yield return new WaitForSeconds(modifier.Duration);
        if (modifier.Duration > 0) {        // If duration <= 0, mod is permanent
            RemoveBuff(type, modifier);
        }
    }

    public void UpdateStats() {
        foreach(StatType type in Enum.GetValues(typeof(StatType))) {
            stats[type].UpdateCalcValue();
        }
    }

    public virtual void Die() { 
        Destroy(gameObject);
    }
}

