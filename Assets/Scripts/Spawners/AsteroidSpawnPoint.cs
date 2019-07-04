using UnityEngine;

public class AsteroidSpawnPoint : MonoBehaviour
{
    public bool HasChildren()
    {
        return transform.childCount > 0 ? true : false;
    }
}
