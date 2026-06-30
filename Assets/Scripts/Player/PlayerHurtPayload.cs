using UnityEngine;

public class PlayerHurtPayload 
{
    public Transform Attacker;
    public int DamageTaken;
    public float ExtraKnockback;
    public PlayerHealth Victim;
}
