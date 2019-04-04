using UnityEngine;

public abstract class StatsModifier : MonoBehaviour 
{
    [SerializeField] int value = 100;

    public int Value { get => value; set => this.value = value; }

    public abstract void Hit();
}