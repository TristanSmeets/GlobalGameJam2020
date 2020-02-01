using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float maxHealth = 100.0f;
    private float health = 100.0f;

    private void OnEnable()
    {
        health = maxHealth;
    }

    public void ChangeHealth(float delta)
    {
        health += delta;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
    }
}
