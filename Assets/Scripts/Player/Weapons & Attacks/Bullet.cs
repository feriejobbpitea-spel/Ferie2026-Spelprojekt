using Unity.Services.Lobbies.Models;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 40;
    public float speed = 20f;
    public float upwardSpeed = 20f;

    public float collisionSize = 2;
    public Vector3 collisionOffset;
    
    public Rigidbody2D rb;
    public PlayerHealth Attacker;

    void Start()
    {
        Vector2 direction = transform.right * speed + Vector3.up * upwardSpeed; // Skjut i den riktning som objektet är vänt + uppåt
        rb.linearVelocity = direction;
    }

    private void Update()
    {
        var hitInfo = Physics2D.OverlapCircle(transform.position + collisionOffset, collisionSize);
        if(hitInfo != null) 
        {
            PlayerHealth playerHealth = hitInfo.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (playerHealth != Attacker)
                {
                    playerHealth.TakeDamage(damage, this.transform);
                    Destroy(gameObject);

                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + collisionOffset, collisionSize);
    }

    // Hejsvejs, böt ut denna mot en collision check i Update istället.
    // Ibland var det så att den flög igenom spelaren
    /*private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth playerHealth = hitInfo.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            if(playerHealth != Attacker)
            {
                playerHealth.TakeDamage(damage, this.transform);
                Destroy(gameObject);

            }
           
        }
       
    }*/

}
