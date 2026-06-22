using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider animatedSlider;

    public void SetMaxHealth(int Health)
    {
        healthSlider.maxValue = Health;
        healthSlider.value = Health;

        animatedSlider.maxValue = Health;
        animatedSlider.value = Health;
    }
    public void SetHealth(int health)
    {
        // Animerad version
        animatedSlider.DOValue(health, 0.3f).SetDelay(.2F);
        
        healthSlider.value = health;
    }
}
