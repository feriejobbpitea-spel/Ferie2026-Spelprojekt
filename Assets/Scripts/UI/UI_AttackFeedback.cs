using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttackFeedback : MonoBehaviour
{
    [SerializeField] private Slider CooldownSlider;
    [SerializeField] private Image IconImage;
    [SerializeField] private TMP_Text KeyToPressText;
    [SerializeField] private Image KeyToPressImage;
    [SerializeField] private SerializedDictionary<string, Sprite> KeyToPressIcons;

    public int Ammo;
    public string KeyToPress;
    public float Cooldown;
    public float MaxCooldown;
    public Sprite CurrentIcon;

    public void Update()
    {
        CooldownSlider.value = Cooldown;
        CooldownSlider.maxValue = MaxCooldown;


        IconImage.sprite = CurrentIcon;

        if (KeyToPressIcons.ContainsKey(KeyToPress))
        {
            KeyToPressText.text = string.Empty;
            KeyToPressImage.sprite = KeyToPressIcons[KeyToPress];
        }
        else 
        {
            KeyToPressText.text = $"{KeyToPress}";
        }
    }
}
