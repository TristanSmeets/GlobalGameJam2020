using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float maxHealth = 100.0f;
    private float currentHealth = 100.0f;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float delta)
    {
        currentHealth += delta;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
