using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttackFeedback : MonoBehaviour
{
    [SerializeField] private Slider CooldownSlider;
    [SerializeField] private Image IconImage;
    [SerializeField] private TMP_Text KeyToPressText;

    public int Ammo;
    public string KeyToPress;
    public float Cooldown;
    public float MaxCooldown;
    public Sprite CurrentIcon;

    public void Update()
    {
        CooldownSlider.value = Cooldown;
        CooldownSlider.maxValue = MaxCooldown;

        KeyToPressText.text = $"{KeyToPress}";

        IconImage.sprite = CurrentIcon;
    }
}
