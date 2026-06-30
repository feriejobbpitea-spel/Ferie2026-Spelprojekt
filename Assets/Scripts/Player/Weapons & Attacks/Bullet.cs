using Unity.Services.Lobbies.Models;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 40;
    public float speed = 20f;
    public float upwardSpeed = 20f;
    public float sineWaveSpeed = 0;
    public float sineWaveHeight = 0;
    public bool UseBoomerang;

    public float collisionSize = 2;
    public Vector3 collisionOffset;
    
    public Rigidbody2D rb;
    public PlayerHealth Attacker;


    public float MaxSurvivalTime;
    private float time;

    void Start()
    {
        Vector2 direction = transform.right * speed + Vector3.up * upwardSpeed; // Skjut i den riktning som objektet är vänt + uppåt
        rb.linearVelocity = direction;

        if(!UseBoomerang)
            GameObject.Destroy(this.gameObject, MaxSurvivalTime);
    }

    private void Update()
    {
        time += Time.deltaTime;

        // används för att åka tillbaka till spelaren efter halva livstiden (boomerang)
        if(time > MaxSurvivalTime / 2 && UseBoomerang) 
        {
            Vector2 newDirection = (Attacker.transform.position - transform.position).normalized;
            rb.linearVelocity = newDirection * speed;
        }

        // ta bort om vi är för nära kastaren
        if(Vector2.Distance(transform.position, Attacker.transform.position) < 1 && time > 1)
        {
            GameObject.Destroy(this.gameObject);
        }

        // används för sinewave attacken
        if (sineWaveSpeed != 0 && sineWaveHeight != 0)
        {
            float sineWaveAffector = Mathf.Sin(Time.time * sineWaveSpeed) * sineWaveHeight;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, sineWaveAffector);
            //transform.position = new Vector2(rb.linearVelocity.x, startHeight + ));
        }

        var hitInfo = Physics2D.OverlapCircle(transform.position + collisionOffset, collisionSize);
        if(hitInfo != null) 
        {
            PlayerHealth playerHealth = hitInfo.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (playerHealth != Attacker)
                {
                    playerHealth.TakeDamage(damage, this.transform, 0);
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
