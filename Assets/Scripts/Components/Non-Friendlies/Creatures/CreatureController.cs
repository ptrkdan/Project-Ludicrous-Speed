
using UnityEngine;

public abstract class CreatureController : EnemyController
{
    [SerializeField] SpawnPointSidePreference spawnPointSidePreference = SpawnPointSidePreference.Both;

    public SpawnPointSidePreference GetSpawnPointSidePreference() => spawnPointSidePreference;
}
