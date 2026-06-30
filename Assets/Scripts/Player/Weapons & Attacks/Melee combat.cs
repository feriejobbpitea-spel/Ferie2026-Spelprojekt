using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class Meleecombat : WeaponBase
{
    private int PlayerID => Player.PlayerID;

    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;
    public PlayerHealth ourPlayerHealth;
    public int attackDamage = 2;
    public float extraKnockback = 0;

    public float cooldownTime = 5f;
    private float cooldownTimer = 0f;

    // Om attacken har en animation kan vi definera det här
    public string AnimationString = "Melee Attack";

    public string InputString = $"Melee Combat - Player";

    // Används för att kommunicera med UI_AttackFeedback för att uppdatera UI för vapen. T.ex. cooldowns, ammo, etc.
    // ------------------------------------------------
    [Header("UI")]
    public string Player1Key = "F";
    public string Player2Key = "K";
    public Sprite UI_Icon;
    [HideInInspector]
    public UI_AttackFeedback UI;
    // ------------------------------------------------

    // --- VFX ---
    public GameObject VFX;
    public float disableVFXAfterTime = 3;
    private bool isActive;

    private void OnDisable()
    {
        isActive = false;
        VFX?.SetActive(false);
    }

    private void Update()
    {
        // Om UI existerar -> Uppdatera den med korrekt info!
        if (UI != null) 
        {
            UI.Cooldown = cooldownTimer;
            UI.MaxCooldown = cooldownTime;
            UI.CurrentIcon = UI_Icon;
            
            string keyToPress = PlayerID == 1 ? Player1Key : Player2Key;
            // Om spelaren använder en handkontroller och är spelare 2
            if(Gamepad.current != null && PlayerID == 2) 
            {
                keyToPress += "C";
            }
            UI.KeyToPress = keyToPress;
        }


        if (Player.CanMove)
        {
            if (cooldownTimer <= 0)
            {
                if (Input.GetButtonDown($"{InputString} {PlayerID}"))
                {
                    Attack();
                    cooldownTimer = cooldownTime;
                }
            }
            else
            {
                cooldownTimer -= Time.deltaTime;
            }
        }
    }
    
    private void Attack() 
    {
        if(VFX != null && isActive == false) 
        {
            isActive = true;
            StartCoroutine(EnableAndDisableVFX());
        }

        UI.PressedButton();

        GetComponent<PlayerAnimations>().Animator.SetTrigger(AnimationString);

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);

        foreach (var enemy in enemiesInRange)
        {
            PlayerHealth enemyplayerHealth = enemy.GetComponent<PlayerHealth>();
            if (enemyplayerHealth == ourPlayerHealth)
                continue;

            enemyplayerHealth.TakeDamage(attackDamage, this.transform, extraKnockback);

        }
    }

    private IEnumerator EnableAndDisableVFX() 
    {
        VFX.SetActive(true);
        yield return new WaitForSeconds(disableVFXAfterTime);
        VFX.SetActive(false);
        isActive = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }



}
