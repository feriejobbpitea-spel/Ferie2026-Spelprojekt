using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerHurt += OnTakeDamage;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerHurt -= OnTakeDamage;
    }

    /// <summary>
    /// Körs då spelaren tar skada
    /// </summary>
    private void OnTakeDamage(PlayerHurtPayload payload) 
    {
        if (payload.Victim.transform != this.transform)
            return;

        animator.SetTrigger($"Hurt");
    }
}
