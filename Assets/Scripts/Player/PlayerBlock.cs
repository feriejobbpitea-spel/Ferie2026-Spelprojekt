using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

 public class PlayerBlock : WeaponBase
{
    public GameObject blockVFXPrefab;
    public GameObject block;
    private int PlayerID => Player.PlayerID;
    public bool isBlocking = false;
    public string InputString = $"Block - Player";

    public float CooldownTime = 5;
    float currentCooldownTime;


    // Används för att kommunicera med UI_AttackFeedback för att uppdatera UI för vapen. T.ex. cooldowns, ammo, etc.
    // ------------------------------------------------
    [Header("UI")]
    public string Player1Key = "F";
    public string Player2Key = "K";
    public Sprite UI_Icon;
    // ------------------------------------------------

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerHurt += OnHurt;
    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerHurt -= OnHurt;
    }

    private void OnHurt(PlayerHurtPayload payload)
    {
        // Gå inte vidare med koden om det inte är vår spelare
        if (payload.Victim.PlayerID != PlayerID)
            return;

        if (!isBlocking)
            return;

        currentCooldownTime = CooldownTime;

        var newBlockVFX = GameObject.Instantiate(blockVFXPrefab, transform.position, Quaternion.identity);
        GameObject.Destroy(newBlockVFX, 1);
    }

    void Update()
    {

        bool holdingButton = Input.GetButton($"{InputString} {PlayerID}");
        bool holdingBumper = Input.GetAxis($"{InputString} {PlayerID}") != 0;

        if ((holdingButton || holdingBumper) && currentCooldownTime <= 0)
        {
            if (isBlocking == false)
            {
                isBlocking = true;
                attackFeedback.PressedButton();
            }
        }
        else
        {
            currentCooldownTime -= Time.deltaTime;
            isBlocking = false;
        }

        // Uppdatera blocken-visuella effekt
        block.SetActive(isBlocking);

        // Om UI existerar -> Uppdatera den med korrekt info!
        if (attackFeedback != null)
        {
            attackFeedback.Cooldown = currentCooldownTime;
            attackFeedback.MaxCooldown = CooldownTime;
            attackFeedback.CurrentIcon = UI_Icon;

            string keyToPress = PlayerID == 1 ? Player1Key : Player2Key;
            // Om spelaren använder en handkontroller och är spelare 2
            if (Gamepad.current != null && PlayerID == 2)
            {
                keyToPress += "C";
            }
            attackFeedback.KeyToPress = keyToPress;
        }
    }  
}
