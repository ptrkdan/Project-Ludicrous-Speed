﻿using System.Collections;
using UnityEngine;

public class EnergyBall : ChargedProjectile
{
    [SerializeField] float sizeIncreaseStep = 1f;
    [SerializeField] float maxSize = 3f;

    [SerializeField] ParticleSystem dissipationVFX;
    [SerializeField] EnergyBallExplosion explosion;
    
    public override void Dissipate()
    {
        base.Dissipate();

        ParticleSystem dissipate = Instantiate(dissipationVFX, transform.position, transform.rotation);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        // Create explosion field that will expand in time around point of collision
        Instantiate(explosion, transform.position, transform.rotation);
    }

    public void IncreaseSize(float amount)
    {
        // Called in animator, once per animation cycle
        if (sizeIncreaseStep < maxSize)
        {
            sizeIncreaseStep += amount;
            GetComponentInChildren<Transform>().localScale += new Vector3(sizeIncreaseStep, sizeIncreaseStep);
        }
        
        if (sizeIncreaseStep >= maxSize)
        {
            isCharged = true;
        }
    }

    public override void DisableCollider()
    {
        GetComponent<CircleCollider2D>().enabled = false;
    }

    public override void EnableCollider()
    {
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
