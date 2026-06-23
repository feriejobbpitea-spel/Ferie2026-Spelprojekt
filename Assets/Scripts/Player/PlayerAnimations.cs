using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        animator.speed = 0.25F;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerHurt += OnTakeDamage;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerHurt -= OnTakeDamage;
    }

    private void Update()
    {
        animator.SetBool("IsMoving", playerMovement.horizontalInput != 0);
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
