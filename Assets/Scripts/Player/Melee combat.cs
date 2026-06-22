using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
public class Meleecombat : MonoBehaviour
{
    [SerializeField] private int PlayerID = 0;
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;
    public PlayerHealth ourPlayerHealth;
    public int attackDamage = 2;

    public float cooldownnTime = 5f;
    private float cooldownTimer = 0f;
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
                cooldownTimer = cooldownnTime;

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
