using UnityEngine;

public class Weapon : MonoBehaviour
{
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
        if (Input.GetButtonDown($"Fire - Player {PlayerID}"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
       GameObject SpawnTheBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SpawnTheBullet.GetComponent<Bullet>().Attacker = Rplayer;
    }
}
