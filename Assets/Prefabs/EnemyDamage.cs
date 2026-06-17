using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //public PlayerHealth PlayerHealth;
    public int damage = 2;
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            //PlayerHealth.TakeDamage(damage);
        }
    }
}
