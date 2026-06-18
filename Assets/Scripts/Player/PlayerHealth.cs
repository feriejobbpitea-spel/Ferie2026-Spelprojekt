using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;


    void Start()
    {
        health = maxHealth;
    }


    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            PlayerManager.Instance.OnPlayerDeath(gameObject);
            health = maxHealth;
        }

    }
}   