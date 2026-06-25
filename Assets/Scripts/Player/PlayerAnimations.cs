using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [HideInInspector]
    public Animator Animator;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        Animator.speed = 0.25F;
    }

    private void OnEnable()
    {
        playerMovement.OnJumped += Player_Jumped;
        playerMovement.OnLanded += Player_Landed;

        PlayerHealth.OnPlayerHurt += OnTakeDamage;
    }

    private void OnDisable()
    {
        playerMovement.OnJumped -= Player_Jumped;
        playerMovement.OnLanded -= Player_Landed;

        PlayerHealth.OnPlayerHurt -= OnTakeDamage;
    }

    private void Update()
    {
        Animator.SetBool("IsFalling", playerMovement.IsFalling());
        Animator.SetBool("IsMoving", playerMovement.horizontalInput != 0);
    }

    private void Player_Landed()
    {
        Animator.SetTrigger("HasLanded");
    }

    private void Player_Jumped()
    {
        Animator.SetTrigger("Jump");
    }


    /// <summary>
    /// Körs då spelaren tar skada
    /// </summary>
    private void OnTakeDamage(PlayerHurtPayload payload) 
    {
        if (payload.Victim.transform != this.transform)
            return;

        Animator.SetTrigger($"Hurt");
    }
}
