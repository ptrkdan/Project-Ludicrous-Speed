using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    public Stat armour;     // HULL
    public Stat shield;     // SHLD
    public Stat speed;      // ENG
    public Stat damage;     // WPN
    public Stat aux;        // AUX

    private void Awake() {
        currentHealth = maxHealth;
    }

    public int Health { get => currentHealth; set => currentHealth = value; }

    protected virtual int CalculateDamage(int damage) {
        return Mathf.Clamp(damage - Mathf.FloorToInt(armour.GetCalcValue()), 0, int.MaxValue);
    }

    public virtual void TakeDamage(int damage) {
            currentHealth -= CalculateDamage(damage);
    }

    public virtual void RepairDamage(int repair) {
        currentHealth = Mathf.Clamp(currentHealth + repair, 0, maxHealth);
    }

    public void SetBuff(BuffType type, StatModifier modifier) {
        StartCoroutine(SetBuffForDuration(type, modifier));
    }

    public void RemoveBuff(BuffType type, StatModifier modifier) {
        switch (type) {
            case BuffType.Hull:
                armour.RemoveModifier(modifier);
                break;
            case BuffType.Shield:
                shield.RemoveModifier(modifier);
                break;
            case BuffType.Engine:
                speed.RemoveModifier(modifier);
                break;
            case BuffType.Weapon:
                damage.RemoveModifier(modifier);
                break;
            case BuffType.Aux:
                aux.RemoveModifier(modifier);
                break;
            case BuffType.None:
            default:
                break;
        }
    }

    IEnumerator SetBuffForDuration(BuffType type, StatModifier modifier) {
        switch (type) {
            case BuffType.Hull:
                armour.AddModifier(modifier);
                break;
            case BuffType.Shield:
                shield.AddModifier(modifier);
                break;
            case BuffType.Engine:
                speed.AddModifier(modifier);
                break;
            case BuffType.Weapon:
                damage.AddModifier(modifier);
                break;
            case BuffType.Aux:
                aux.AddModifier(modifier);
                break;
            case BuffType.None:
            default:
                break;
        }
        yield return new WaitForSeconds(modifier.Duration);

        if (modifier.Duration > 0) {        // If duration <= 0, mod is permanent
            RemoveBuff(type, modifier);
        }
    }

    public virtual void Die() { 
        Destroy(gameObject);
    }
}

