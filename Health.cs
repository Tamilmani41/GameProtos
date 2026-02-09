using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    // UI can subscribe to this event to update health display
    public event Action<int, int> OnHealthChanged; 

    private void Awake()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        //Debug.Log("HIT"+ currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false); // pooled enemies later
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
