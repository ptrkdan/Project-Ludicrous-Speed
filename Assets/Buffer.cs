using System;
using UnityEngine;

public class Buffer : PickUp
{
    [SerializeField] float buffValue = 1f;
    [SerializeField] float duration = 5f;
    [SerializeField] StatModType modType;
    [SerializeField] BuffType buffType;

    public float BuffValue { get => buffValue; set => this.buffValue = value; }
    public float Duration { get => duration; set => duration = value; }

    public override void Interact(Interactable other) {
        base.Interact(other);
        if (other.GetType().IsSubclassOf(typeof(LivingInteractable))) {
            Buff((LivingInteractable)other);
        }
    }

    private void Buff(LivingInteractable other) {
        StatModifier mod = new StatModifier(buffValue, modType, duration);
        other.SetBuff(buffType, mod);
    }
}
