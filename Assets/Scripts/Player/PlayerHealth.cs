using System;
using Unity.Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
 
    public int PlayerID => GetComponent<Player>()?.PlayerID ?? -1;


   


    [SerializeField] private AudioClip[] damageSoundClip;
    
    public HealthBar healthBar;
    public PlayerBlock playerblock;

    public static event Action OnKubbkingDied;
    public static event Action<PlayerHealth> OnPlayerDied;
    public static event Action<PlayerHurtPayload> OnPlayerHurt;
    public static event Action<PlayerHealth> OnPlayerHealthChanged;

   
    void Start()
    {
        ResetPlayerHealth();
       
       
    }

    public void ResetPlayerHealth() 
    {
        health = maxHealth;
        OnPlayerHealthChanged?.Invoke(this);
    }

    public void TakeDamage(int amount, Transform attacker, float extraKnockback)
    {
        if (playerblock?.isBlocking == true)
        {
            //amount -= amount / 2;
            amount = 0;
            return;
        }

        health -= amount;

         
        
           
        // se till att hälsan inte går under 0 eller över maxhälsan
        health = Mathf.Clamp(health, 0, maxHealth);


        var hurtPayload = new PlayerHurtPayload
        {
            Attacker = attacker,
            DamageTaken = amount,
            Victim = this,
            ExtraKnockback = extraKnockback

        };

        // Skicka till eventet att spelaren har tagit skada, så att andra scripts kan reagera på det
        OnPlayerHurt?.Invoke(hurtPayload);


        if (health <= 0)
        {
            if (PlayerID == 1) 
            {
                ScoreBoard.instance.Player1_WonRound();
                PlayerManager.Instance.OnPlayerDeath(gameObject);
                OnPlayerDied?.Invoke(this);
            }
            else if (PlayerID == 2)
            {
                ScoreBoard.instance.Player2_WonRound();
                PlayerManager.Instance.OnPlayerDeath(gameObject);
                OnPlayerDied?.Invoke(this);
            }
            else 
            {
                OnKubbkingDied?.Invoke();
            }
        }
        else 
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(damageSoundClip, transform, 1f);
        }


        //healthBar.SetHealth(health);
        OnPlayerHealthChanged?.Invoke(this);

       
    }
}   