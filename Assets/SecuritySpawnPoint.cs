using UnityEngine;

public class SecuritySpawnPoint : MonoBehaviour
{
    public bool HasChildren()
    {
        return transform.childCount > 0 ? true : false;
    }
}
