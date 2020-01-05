using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Credits")]
public class Credits : LootConfig
{
    [SerializeField] private Vector2Int creditValueRange;

    public int GetCreditValue()
    {
        creditValue = Random.Range(creditValueRange.x, creditValueRange.y);
        return creditValue;
    }
}
