using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float cooldownTime = 3f;
    private float cooldownTimer = 0f;
    public PlayerHealth Rplayer;
    [SerializeField] private int PlayerID = 0;
    public Transform firePoint;
    public GameObject bulletPrefab;
   
    

    
    void Update()
    {
       if (cooldownTimer <= 0)
       {


            if (Input.GetButtonDown($"Fire - Player {PlayerID}"))
            {
                Shoot();
                cooldownTimer = cooldownTime;
            }

           
       }
       else
       {
          cooldownTimer -= Time.deltaTime;
       }
    }



    void Shoot()
    {
       GameObject SpawnTheBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SpawnTheBullet.GetComponent<Bullet>().Attacker = Rplayer;
    }
}
