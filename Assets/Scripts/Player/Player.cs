using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action<Player> OnCreated;

    public int PlayerID = 1;

    public void Start()
    {
        OnCreated?.Invoke(this);
    }
}
