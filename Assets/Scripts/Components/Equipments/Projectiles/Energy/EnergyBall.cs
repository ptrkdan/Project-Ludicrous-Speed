using System.Collections;
using UnityEngine;

public class EnergyBall : ChargedProjectile
{
    [SerializeField] float sizeIncreaseStep = 1f;
    [SerializeField] float maxSize = 3f;
    [SerializeField] ParticleSystem dissipationVFX;
    [SerializeField] float disspateVFXDelay = 1f;
    
    public override void Dissipate()
    {
        base.Dissipate();

        ParticleSystem dissipate = Instantiate(dissipationVFX, transform.position, transform.rotation);
        Destroy(dissipate.gameObject, disspateVFXDelay);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Create explosion field that will expand in time around point of collision
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
