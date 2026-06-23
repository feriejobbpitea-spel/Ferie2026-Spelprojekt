using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int PlayerID = 1;

    public Slider healthSlider;
    public Slider animatedSlider;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerHealthChanged += OnPlayerHealthChanged;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerHealthChanged -= OnPlayerHealthChanged;
    }

    public void SetMaxHealth(int Health)
    {
        healthSlider.maxValue = Health;
        //healthSlider.value = Health;

        animatedSlider.maxValue = Health;
        //animatedSlider.value = Health;
    }
    public void SetHealth(int health)
    {
        // Animerad version
        animatedSlider.DOValue(health, 0.3f).SetDelay(.2F);
        
        healthSlider.value = health;
    }

    private void OnPlayerHealthChanged(PlayerHealth health)
    {
        if (health.GetComponent<Player>().PlayerID != PlayerID)
            return;

        SetMaxHealth(health.maxHealth);
        SetHealth(health.health);
    }
}
