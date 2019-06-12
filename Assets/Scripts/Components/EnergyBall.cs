using UnityEngine;

public class EnergyBall : ChargedProjectile
{
    float size = 1f;
    float maxSize = 5f;

    public void IncreaseSize(float amount)
    {
        if (size < maxSize)
        {
            size += amount;
            GetComponentInChildren<Transform>().localScale += new Vector3(size, size);
        }
        else
        {
            isCharged = true;
        }
    }
}
