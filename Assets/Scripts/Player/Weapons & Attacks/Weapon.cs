using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private float cooldownTime = 3f;
    private float cooldownTimer = 0f;
    public PlayerHealth Rplayer;
    
    private int PlayerID => player.PlayerID;
    private Player player;

    public Transform firePoint;
    public GameObject bulletPrefab;

    // Används för att kommunicera med UI_AttackFeedback för att uppdatera UI för vapen. T.ex. cooldowns, ammo, etc.
    // ------------------------------------------------
    [Header("UI")]
    public string Player1Key = "F";
    public string Player2Key = "K";
    public Sprite UI_Icon;
    [HideInInspector]
    public UI_AttackFeedback UI;
    // ------------------------------------------------

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
   
    

    
    void Update()
    {
        // Om UI existerar -> Uppdatera den med korrekt info!
        if (UI != null)
        {
            UI.Cooldown = cooldownTimer;
            UI.MaxCooldown = cooldownTime;
            UI.CurrentIcon = UI_Icon;

            string keyToPress = PlayerID == 1 ? Player1Key : Player2Key;
            // Om spelaren använder en handkontroller och är spelare 2
            if (Gamepad.current != null && PlayerID == 2)
            {
                keyToPress += "C";
            }
            UI.KeyToPress = keyToPress;
        }

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
        UI.PressedButton();

        GetComponent<PlayerAnimations>().Animator.SetTrigger("Throw");

        GameObject SpawnTheBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SpawnTheBullet.GetComponent<Bullet>().Attacker = Rplayer;
    }
}
