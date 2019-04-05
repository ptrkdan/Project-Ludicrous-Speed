using System;
using UnityEngine;

public class Buffer : Interactable
{
    [SerializeField] float modifier = 1f;
    [SerializeField] float duration = 5f;
    [SerializeField] BuffType buffType = BuffType.None;

    public float Modifier { get => modifier; set => modifier = value; }
    public float Duration { get => duration; set => duration = value; }

    public override void Interact(Interactable other) {
        base.Interact(other);
        if (other.GetType().IsSubclassOf(typeof(LivingInteractable))) {
            Buff((LivingInteractable)other);
        }
    }

    private void Buff(LivingInteractable other) {
        other.SetBuff(buffType, modifier, duration);
    }
}
