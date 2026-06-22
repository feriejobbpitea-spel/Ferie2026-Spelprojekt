using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerHealth Rplayer;
    [SerializeField] private int PlayerID = 0;
    public Transform firePoint;
    public GameObject bulletPrefab;

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
