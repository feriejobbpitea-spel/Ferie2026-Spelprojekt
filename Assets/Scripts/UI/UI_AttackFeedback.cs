using AYellowpaper.SerializedCollections;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttackFeedback : MonoBehaviour
{
    [SerializeField] private Slider CooldownSlider;
    [SerializeField] private Image IconImage;
    
    [SerializeField] private GameObject KeyToPressWrapper;
    [SerializeField] private TMP_Text KeyToPressText;

    [SerializeField] private Image ConsoleKeyToPressImage;
    
    // Första spriten är default
    // Andra sprites är animation för klick
    [SerializeField] private SerializedDictionary<string, Sprite[]> KeyToPressIcons;

    public int Ammo;
    public string KeyToPress;
    public float Cooldown;
    public float MaxCooldown;
    public Sprite CurrentIcon;

    private bool playingAnimation;

    public void Update()
    {
        CooldownSlider.value = Cooldown;
        CooldownSlider.maxValue = MaxCooldown;


        IconImage.sprite = CurrentIcon;

        if (KeyToPressIcons.ContainsKey(KeyToPress))
        {
            KeyToPressWrapper.SetActive(false);
            ConsoleKeyToPressImage.enabled = true;
            
            if (!playingAnimation)
                ConsoleKeyToPressImage.sprite = KeyToPressIcons[KeyToPress][0];
        }
        else 
        {
            KeyToPressWrapper.SetActive(true);
            ConsoleKeyToPressImage.enabled = false;

            KeyToPressText.text = $"{KeyToPress}";
        }
    }

    public void PressedButton() 
    {
        if (!KeyToPressIcons.ContainsKey(KeyToPress))
            return;
        playingAnimation = true;

        StartCoroutine(PlayClickAnimation());
    }

    private IEnumerator PlayClickAnimation() 
    {

        // Gå igenom varje sprite i animationen
        foreach (var item in KeyToPressIcons[KeyToPress])
        {
            ConsoleKeyToPressImage.sprite = item;

            yield return new WaitForSeconds(0.05F);
        }

        // Återställ till normal
        ConsoleKeyToPressImage.sprite = KeyToPressIcons[KeyToPress][0];
    }
}
