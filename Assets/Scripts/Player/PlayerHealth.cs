using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;

    public int PlayerID => GetComponent<Player>().PlayerID;

    public HealthBar healthBar;

    public static event Action<PlayerHealth> OnPlayerDied;
    public static event Action<PlayerHurtPayload> OnPlayerHurt;
    public static event Action<PlayerHealth> OnPlayerHealthChanged;

    void Start()
    {
        health = maxHealth;
        OnPlayerHealthChanged?.Invoke(this);
    }


    public void TakeDamage(int amount, Transform attacker)
    {
        health -= amount;

        // Skicka till eventet att spelaren har tagit skada, så att andra scripts kan reagera på det
        OnPlayerHurt?.Invoke(new PlayerHurtPayload
        {
            Attacker = attacker,
            DamageTaken = amount,
            Victim = this
        });

        if (health <= 0)
        {
            PlayerManager.Instance.OnPlayerDeath(gameObject);
            OnPlayerDied?.Invoke(this);
            health = maxHealth;
        }

        //healthBar.SetHealth(health);
        OnPlayerHealthChanged?.Invoke(this);
    }
}   