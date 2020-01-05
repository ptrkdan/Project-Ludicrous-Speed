using UnityEngine;

public class Buffer : PickUp
{
    [SerializeField] private float buffValue = 1f;
    [SerializeField] private float duration = 5f;
    [SerializeField] private StatModType modType;
    [SerializeField] private StatType buffType;

    public float BuffValue { get => buffValue; set => this.buffValue = value; }
    public float Duration { get => duration; set => duration = value; }

    public override void Interact(Interactable other)
    {
        base.Interact(other);
        if (other.GetType().IsSubclassOf(typeof(LivingInteractable)))
        {
            Buff((LivingInteractable)other);
        }
    }

    private void Buff(LivingInteractable other)
    {
        StatModifier mod = new StatModifier(gameObject, buffType, modType, buffValue, duration);
        other.SetBuff(buffType, mod);
    }
}
