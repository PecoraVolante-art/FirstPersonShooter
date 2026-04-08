using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float CurrentHealth, MaxHealth, Width, Height;

    [SerializeField] private RectTransform healthBar;

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        if (MaxHealth <= 0) return;

        CurrentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);

        float newWidth = (CurrentHealth / MaxHealth) * Width;

        healthBar.sizeDelta = new Vector2(newWidth, Height);
        healthBar.anchoredPosition = new Vector2(0, healthBar.anchoredPosition.y);
    }
}
