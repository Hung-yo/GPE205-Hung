using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public Death death;
    public event System.Action<float, GameObject> OnDamaged;

    public void TakeDamage(float amount, GameObject damageSource)
    {
        currentHealth -= amount;
        OnDamaged?.Invoke(amount, damageSource);
        if (currentHealth <= 0)
        {
            death.Die();
        }

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            death.Die();
        }
        
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
