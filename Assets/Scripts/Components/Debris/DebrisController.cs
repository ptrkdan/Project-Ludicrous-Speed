using System;
using UnityEngine;

public class DebrisController : LivingInteractable
{

    Rigidbody2D rigidBody;

    protected override void Initialize()
    {
        base.Initialize();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>().DealDamage(other);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    private void Move()
    {
        rigidBody.MovePosition(transform.position - Vector3.right * stats.GetStat(StatType.Engine).GetCalcValue() * Time.fixedDeltaTime);
    }
}
