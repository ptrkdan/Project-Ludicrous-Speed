using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class LivingInteractable : Interactable {
    protected CharacterStats stats;

    private void Awake() {
        stats = GetComponent<CharacterStats>();
    }

    public virtual void TakeDamage(int damage) {
        stats.TakeDamage(damage);
    }
    public virtual void RepairDamage(int repair) {
        stats.RepairDamage(repair);
    }

    public void SetBuff(BuffType stat, StatModifier modifier) {
        stats.SetBuff(stat, modifier);
    }
}
