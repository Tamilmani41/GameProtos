using UnityEngine;
// This script manages enemy stats and applies wave-based scaling to health and damage.
// This script does not know about waves itself, it just receives the wave number from an external source (like a WaveManager).
public class EnemyStats : MonoBehaviour
{
    public int baseHealth = 3;
    public int baseDamage = 1;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public void ApplyWaveScaling(int waveNumber)
    {
        if (health == null)
        {
            Debug.Log("Enemy stats: health omponent missing!", this);
            return;
        }
        int scaledHealth = baseHealth + waveNumber;
        int scaledDamage = baseDamage + (waveNumber / 2);

        health.maxHealth = scaledHealth;
        health.ResetHealth();

        // later you can pass damage to bullets here
    }
}
