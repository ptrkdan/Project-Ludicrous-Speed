using UnityEngine;

public class Credits : LootConfig
{
    [SerializeField] Vector2Int creditValueRange;

    public int GetCreditValue() => Random.Range(creditValueRange.x, creditValueRange.y);
}
