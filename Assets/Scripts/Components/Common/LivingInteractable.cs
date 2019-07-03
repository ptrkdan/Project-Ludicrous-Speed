using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InteractableStats))]
public class LivingInteractable : Interactable {
    protected InteractableStats stats;
    private float onCollisionGlowDuration = 0.15f;

    private void Awake() {
        stats = GetComponent<InteractableStats>();
        Initialize();
    }

    protected virtual void Initialize() { }

    public virtual void TakeDamage(float damage) {
        stats.TakeDamage(damage);
    }
    public virtual void RepairDamage(float repair) {
        stats.RepairDamage(repair);
    }

    public void SetBuff(StatType stat, StatModifier modifier) {
        stats.SetBuff(stat, modifier);
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        StartCoroutine(Glow());
    }

    IEnumerator Glow() {
        GetComponentInChildren<SpriteRenderer>().color = Color.HSVToRGB(0, 0, 1);

        yield return new WaitForSeconds(onCollisionGlowDuration);

        GetComponentInChildren<SpriteRenderer>().color = Color.HSVToRGB(0, 0, 0.8f);
    }
}
