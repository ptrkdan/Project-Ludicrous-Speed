using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    public Stat armour;     // HULL
    public Stat shield;     // SHLD
    public Stat speed;      // ENG
    public Stat damage;     // WPN
    public Stat auxiliary;  // AUX

    private void Awake() {
        currentHealth = maxHealth;
    }

    public int Health { get => currentHealth; set => currentHealth = value; }

    protected virtual int CalculateDamage(int damage) {
        return Mathf.Clamp(damage - Mathf.FloorToInt(armour.GetCalculatedValue()), 0, int.MaxValue);
    }

    public virtual void TakeDamage(int damage) {
        currentHealth -= CalculateDamage(damage);
    }

    public virtual void RepairDamage(int repair) {
        currentHealth = Mathf.Clamp(currentHealth + repair, 0, maxHealth);
    }

    public virtual void Die() { 
        Destroy(gameObject);
    }
}
