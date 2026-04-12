using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public int scoreAmount = 1;
    public Death death;
    public AudioClip takeDamageSound;
    public AudioClip healSound;
    public AudioSource audioSource;
    public event System.Action<float, GameObject> OnDamaged;
    public event System.Action<float, float, GameObject> OnHealthChanged;

    public void TakeDamage(float amount, GameObject damageSource)
    {
        currentHealth -= amount;
        OnDamaged?.Invoke(amount, damageSource);
        OnHealthChanged?.Invoke(currentHealth, maxHealth, gameObject);
        if (currentHealth <= 0)
        {
            PawnTank pawnTank = damageSource.GetComponent<PawnTank>();

            if (pawnTank.playerID == "p1")
            {
                GameManager.IncreaseScore(scoreAmount, "p1");
            }
            else if (pawnTank.playerID == "p2")
            {
                GameManager.IncreaseScore(scoreAmount, "p2");
            }
            
            death.Die();
        }

    }

    public void TakeDamage(float amount)
    {
        if (takeDamageSound != null)
        {
            audioSource.PlayOneShot(takeDamageSound);
        }
        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth, maxHealth, gameObject);
        if (currentHealth <= 0)
        {
            death.Die();
        }
        
    }

    public void Heal(float amount)
    {
        if (healSound != null)
        {
            audioSource.PlayOneShot(healSound);
        }
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke(currentHealth, maxHealth, gameObject);
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        OnHealthChanged?.Invoke(currentHealth, maxHealth, gameObject);
    }
}
