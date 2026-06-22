using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerKnockback : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerHurt += OnPlayerHurt;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerHurt -= OnPlayerHurt;
    }

    private void OnPlayerHurt(PlayerHurtPayload payload)
    {
        // Ge inte spelaren knockbak om den är död
        if (payload.Victim.health <= 0)
            return;

        if (payload.Victim.transform != this.transform)
            return;

        Vector2 direction = (payload.Victim.transform.position - payload.Attacker.position).normalized;
        direction.y = 0;
        playerMovement.ApplyForce(direction);
    }
}
