using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InteractableStats))]
public class LivingInteractable : Interactable
{
    private const float ON_COLLISION_GLOW_DURATION = 0.15f;

    protected Behaviour[] behaviours;
    protected BehaviourState behaviourState;
    protected InteractableStats stats;

    #region Methods: Unity

    private void Awake()
    {
        stats = GetComponent<InteractableStats>();
        Initialize();
    }

    private void Update()
    {
        foreach (Behaviour behaviour in behaviours)
        {
            behaviourState = behaviour.Do();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        StartCoroutine(Glow());
    }

    #endregion Methods: Unity

    protected virtual void Initialize()
    {
        behaviours = GetComponents<Behaviour>();
    }

    #region Methods: Stats

    public virtual void TakeDamage(float damage)
    {
        stats.TakeDamage(damage);
    }
    public virtual void RepairDamage(float repair)
    {
        stats.RepairDamage(repair);
    }

    public void SetBuff(StatType stat, StatModifier modifier)
    {
        stats.SetBuff(stat, modifier);
    }

    #endregion Methods: Stats

    private IEnumerator Glow()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.HSVToRGB(0, 0, 1);

        yield return new WaitForSeconds(ON_COLLISION_GLOW_DURATION);

        GetComponentInChildren<SpriteRenderer>().color = Color.HSVToRGB(0, 0, 0.8f);
    }
}
