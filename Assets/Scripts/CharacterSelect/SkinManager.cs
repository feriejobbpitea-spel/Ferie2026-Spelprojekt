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
    [SerializeField] TextMeshProUGUI Health;
    [SerializeField] TextMeshProUGUI Kick;
    [SerializeField] TextMeshProUGUI Slash;
    [SerializeField] TextMeshProUGUI ooooo;
    [SerializeField] private AudioClip AttackSoundClip;


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

    public void NextOption()
    {
        selectedSkin = selectedSkin + 1;
        if (selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        
        
        int maxHealth = skins[selectedSkin].GetComponent<PlayerHealth>().maxHealth;
        Health.text = maxHealth.ToString();

        skins[selectedSkin].GetComponents<Meleecombat>();


        Meleecombat[] Melee = skins[selectedSkin].GetComponents<Meleecombat>();

        Kick.text = Melee[0].attackDamage.ToString();
        Slash.text = Melee[1].attackDamage.ToString();

        string namn = skins[selectedSkin].GetComponent<Player>().Name;
        ooooo.text = namn.ToString();


        sr.sprite = skins[selectedSkin].GetComponent<SpriteRenderer>().sprite;

        if (PlayerID == 1)
        {
            PlayerManager.player1ID = selectedSkin;
        }

        if (PlayerID == 2)
        {
            PlayerManager.player2ID = selectedSkin;

        }
    }

    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count -1;
        }

        int maxHealth = skins[selectedSkin].GetComponent<PlayerHealth>().maxHealth;
        Health.text = maxHealth.ToString();

        Meleecombat[] Melee = skins[selectedSkin].GetComponents<Meleecombat>();
        Kick.text = Melee[0].attackDamage.ToString();
        Slash.text = Melee[1].attackDamage.ToString();

        string namn = skins[selectedSkin].GetComponent<Player>().Name;
        ooooo.text = namn.ToString();

        sr.sprite = skins[selectedSkin].GetComponent<SpriteRenderer>().sprite;
        ;

        if (PlayerID == 1)
        {
            PlayerManager.player1ID = selectedSkin;
        }

        if (PlayerID == 2)
        {
            PlayerManager.player2ID = selectedSkin;

        }
    }
}
