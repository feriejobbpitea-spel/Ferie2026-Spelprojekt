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
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        /*if (IsOwner)
        {
            PingRpc(health);
        }*/
     
    }
    /*[Rpc(SendTo.Server)]
    public void PingRpc(int pingCount)
    {
        // Server -> Clients because PongRpc sends to NotServer
        // Note: This will send to all clients.
        // Sending to the specific client that requested the pong will be discussed in the next section.
        PongRpc(pingCount, "PONG!");
    }

    [Rpc(SendTo.NotServer)]
    void PongRpc(int pingCount, string message)
    {
        Debug.Log($"Received pong from server for ping {pingCount} and message {message}");

    }*/
}   