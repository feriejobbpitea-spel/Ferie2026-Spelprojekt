using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float cooldownTime = 3f;
    private float cooldownTimer = 0f;
    public PlayerHealth Rplayer;
    
    private int PlayerID => player.PlayerID;
    private Player player;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
   
    

    
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
