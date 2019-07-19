using System;
using UnityEngine;

public class DebrisController : LivingInteractable
{
    protected override void Initialize()
    {
        base.Initialize();
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>().DealDamage(other);
    }
}
