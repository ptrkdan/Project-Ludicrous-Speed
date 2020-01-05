using UnityEngine;

public class CreatureSpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnPointSide side;

    public bool HasChildren()
    {
        return transform.childCount > 0;
    }
}

public enum SpawnPointSide { Left, Right }
public enum SpawnPointSidePreference { Left, Right, Both }
