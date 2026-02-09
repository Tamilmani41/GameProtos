using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public Health enemyHealth;
    public Slider slider;
    public Vector3 offset = new Vector3(0, 1.2f, 0);

    private void Start()
    {
        enemyHealth.OnHealthChanged += UpdateHealth;
        UpdateHealth(enemyHealth.maxHealth, enemyHealth.maxHealth);
    }

    private void LateUpdate()
    {
        if (enemyHealth != null)
        {
            transform.position = enemyHealth.transform.position + offset;
            transform.rotation = Quaternion.identity;
        }
    }

    private void UpdateHealth(int current, int max)
    {
        slider.value = (float)current / max;
    }

    private void OnDestroy()
    {
        enemyHealth.OnHealthChanged -= UpdateHealth;
    }
}