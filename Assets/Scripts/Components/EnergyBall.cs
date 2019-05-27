using UnityEngine;

public class EnergyBall : Projectile
{
    float size = 1f;
    
    public void IncreaseSize(float amount)
    {
        size += amount;
        GetComponentInChildren<Transform>().localScale = new Vector2(size, size);
        if (size > 10f)
        {
            Fire();
        }
    }
}
