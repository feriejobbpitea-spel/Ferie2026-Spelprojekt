using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SkinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CharacterSpeed;
    [SerializeField] TextMeshProUGUI CharacterHealth;
    [SerializeField] TextMeshProUGUI CharacterName;
    
    [SerializeField] TextMeshProUGUI KubbpinneType;
    [SerializeField] TextMeshProUGUI KubbpinneDescription;

    [SerializeField] private AudioClip AttackSoundClip;

    private float cooldownTimer;
    private float cooldownTime = 0.2f;
    public bool characterselet;
    public int PlayerID;   
    public Image sr;
    public List<Player> skins = new List<Player>();
    private int selectedSkin = 0;
    
    private void Awake()
    {
        NextOption();
        
    }
    private void Start()
    {
        SoundFXManager.instance.PlaySoundFXClip(AttackSoundClip, transform, 1f);
    }

    
    private void Update()
    {
        if (characterselet == true)
        {

            float input = Input.GetAxisRaw("Horizontal - Player 2");
            if (cooldownTimer <= 0)
            {
              

                if (input > 0f)
                {
                  NextOption();
                    
                }

               

                if (input < 0f)
                {
                  BackOption();
                   
                }
                cooldownTimer = cooldownTime;
            }
            else
            {
                cooldownTimer -= Time.deltaTime;
            }
        }
     
    }
    public void NextOption()
    {
       
         selectedSkin = selectedSkin + 1;
         if (selectedSkin == skins.Count)
         {
            selectedSkin = 0;
         }


        if (PlayerID == 1)
        {
            PlayerManager.player1ID = selectedSkin;
        }

        if (PlayerID == 2)
        {
            PlayerManager.player2ID = selectedSkin;

        }
        UpdateUI();
    }

    public void BackOption()
    {
       
            selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count -1;
        }

        if (PlayerID == 1)
        {
            PlayerManager.player1ID = selectedSkin;
        }

        if (PlayerID == 2)
        {
            PlayerManager.player2ID = selectedSkin;

        }

        UpdateUI();
    }

    private void UpdateUI() 
    {
        // Uppdatera hälsa        
        int maxHealth = skins[selectedSkin].GetComponent<PlayerHealth>().maxHealth;
        CharacterHealth.text = $"Health: {maxHealth}";

        // Updatera karaktärens namn
        string namn = skins[selectedSkin].GetComponent<Player>().Name;
        CharacterName.text = $"{namn}";

        // Updatera karaktärens hastighet
        float speed = skins[selectedSkin].GetComponent<PlayerMovement>().moveSpeed;
        CharacterSpeed.text = $"Speed: {speed}";

        // Updatera karaktärens kubbpinne-typ
        GameObject bulletPrefab = skins[selectedSkin].GetComponent<Weapon>().bulletPrefab;
        
        /// Jag har lagt till en kubbpinne script på alla kubbpinnar där vi kan fylla in lite data!
        Kubbpinne kubbpinneData = bulletPrefab.GetComponent<Kubbpinne>();

        if (kubbpinneData != null)
        {
            KubbpinneType.text = $"{kubbpinneData.Name}";
            KubbpinneDescription.text = $"{kubbpinneData.Description}";
        }

        // Uppdatera karaktärens bild
        sr.sprite = skins[selectedSkin].GetComponent<SpriteRenderer>().sprite;


        /*        Meleecombat[] Melee = skins[selectedSkin].GetComponents<Meleecombat>();

                CharacterSpeed.text = Melee[0].attackDamage.ToString();
                CharacterHealth.text = Melee[1].attackDamage.ToString();*/
    }
}
