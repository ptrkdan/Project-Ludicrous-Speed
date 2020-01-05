using UnityEngine;

public abstract class CreatureController : EnemyController
{
    [SerializeField] private SpawnPointSidePreference spawnPointSidePreference = SpawnPointSidePreference.Both;

    public SpawnPointSidePreference GetSpawnPointSidePreference() => spawnPointSidePreference;
}
