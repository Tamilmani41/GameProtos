using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health targetHealth;
    public Slider slider;

    private void Start()
    {
        if (targetHealth != null)
        {
            targetHealth.OnHealthChanged += UpdateUI;
        }
    }

    private void UpdateUI(int current, int max)
    {
        slider.value = (float)current / max;
    }

    private void OnDestroy()
    {
        if (targetHealth != null)
        {
            targetHealth.OnHealthChanged -= UpdateUI;
        }
    }
}

