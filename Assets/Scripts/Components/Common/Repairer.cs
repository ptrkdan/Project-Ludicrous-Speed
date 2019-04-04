using UnityEngine;

public class Repairer : MonoBehaviour
{
    [SerializeField] int repairValue = 100;

    public int RepairValue { get => repairValue; set => repairValue = value; }

    public void Hit() {
        Destroy(gameObject);
    }
}
