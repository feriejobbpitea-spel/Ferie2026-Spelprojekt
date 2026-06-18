using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Här letar vi om det finns en playerhealth component på det objekt vi kolliderade med, och om det finns så skadar vi den
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Här letar vi om det finns en playerhealth component på det objekt vi kolliderade med, och om det finns så skadar vi den
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Här letar vi om det finns en playerhealth component på det objekt vi kolliderade med, och om det finns så skadar vi den
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
