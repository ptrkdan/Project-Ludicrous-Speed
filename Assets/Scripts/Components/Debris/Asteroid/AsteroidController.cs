using UnityEngine;

public class AsteroidController : DebrisController
{
    private void Start()
    {
        FindObjectOfType<AsteroidSpawner>().IncreaseAsteroidCount();
    }

    private void OnDestroy()
    {
        FindObjectOfType<AsteroidSpawner>().DecreaseAsteroidCount();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
       base.OnTriggerEnter2D(other);
    }
}
