using Unity.Services.Lobbies.Models;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 40;
    public float speed = 20f;
    public Rigidbody2D rb;
    public PlayerHealth Attacker;

    void Start()
    {
        Vector2 direction = transform.right + Vector3.up; // Skjut i den riktning som objektet är vänt + uppåt
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
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
       
    }
    
}
