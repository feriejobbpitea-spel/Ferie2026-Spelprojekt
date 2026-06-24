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
        playerMovement.OnJumped += Player_Jumped;
        playerMovement.OnReachedPeak += Player_ReachedPeak;
        playerMovement.OnLanded += Player_Landed;

        PlayerHealth.OnPlayerHurt += OnTakeDamage;
    }

    private void OnDisable()
    {
        playerMovement.OnJumped -= Player_Jumped;
        playerMovement.OnReachedPeak -= Player_ReachedPeak;

        PlayerHealth.OnPlayerHurt -= OnTakeDamage;
    }

    private void Update()
    {
        animator.SetBool("IsMoving", playerMovement.horizontalInput != 0);
    }

    private void Player_Landed()
    {
        animator.SetTrigger("HasLanded");
    }

    private void Player_ReachedPeak()
    {
        animator.SetTrigger("HasReachedPeakJump");
    }

    private void Player_Jumped()
    {
        animator.SetTrigger("HasJumped");
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
