using UnityEngine;

public class AsteroidController : DebrisController
{
    protected override void Initialize()
    {
        base.Initialize();
        FindObjectOfType<AsteroidSpawner>().IncreaseAsteroidCount();
    }

    private void OnDestroy()
    {
        FindObjectOfType<AsteroidSpawner>()?.DecreaseAsteroidCount();
    }
}
