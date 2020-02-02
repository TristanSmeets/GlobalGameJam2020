using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float maxHealth = 100.0f;
    private float currentHealth = 100.0f;
    private Player.Player player;

    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Player.Player>();
        currentHealth = maxHealth;
        GameStats.OnWaveEnd += HealPlayer;
        GameStats.OnRoundEnd += HealPlayerFull;
    }

    private void OnDisable()
    {
        GameStats.OnWaveEnd -= HealPlayer;
        GameStats.OnRoundEnd -= HealPlayerFull;
    }

    private void HealPlayer()
    {
        ChangeHealth(GetMaxHealth() * 0.2f);
        player.TakeDamage(0);
    }

    private void HealPlayerFull()
    {
        ChangeHealth(GetMaxHealth());
        player.TakeDamage(0);
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

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
