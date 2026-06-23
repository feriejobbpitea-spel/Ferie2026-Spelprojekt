using UnityEngine;

[RequireComponent(typeof(Player))]
public class Meleecombat : MonoBehaviour
{
    private int PlayerID => player.PlayerID;
    private Player player;

    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;
    public PlayerHealth ourPlayerHealth;
    public int attackDamage = 2;

    public float cooldownTime = 5f;
    private float cooldownTimer = 0f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (cooldownTimer <= 0) {


            if (Input.GetButtonDown($"Melee Combat - Player {PlayerID}"))
            {
                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);

                foreach (var enemy in enemiesInRange)
                {
                    PlayerHealth enemyplayerHealth = enemy.GetComponent<PlayerHealth>();
                    if (enemyplayerHealth == ourPlayerHealth)
                        continue;

                    enemyplayerHealth.TakeDamage(attackDamage, this.transform);
                    
                }
                cooldownTimer = cooldownTime;

            }

        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }



}
