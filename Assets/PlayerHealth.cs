using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public int health;
    public int maxHealth = 10;


    void Start()
    {
        health = maxHealth;
    }


    public void TakeDamage(int amount)

    {
        if (!IsOwner)
            return;

        if (health <= 0)
            return;

        PingRpc(health);

        /*if (IsOwner)
        {
            PingRpc(health);
        }*/

    }
    [Rpc(SendTo.Server)]
    public void PingRpc(int pingCount)
    {
        // Server -> Clients because PongRpc sends to NotServer
        // Note: This will send to all clients.
        // Sending to the specific client that requested the pong will be discussed in the next section.
        print(pingCount);
        health -= pingCount;
        if (health <= 0)
        {
            PlayerManager.Instance.OnPlayerDeath(gameObject);
            health = maxHealth;
        }

        PongRpc(pingCount);
    }

    [Rpc(SendTo.NotServer)]
    void PongRpc(int pingCount)
    {
        Debug.Log($"Received pong from server for ping {pingCount}");
    }
}   