using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public static Action<Player> OnCreated;

    public int PlayerID = 1;

    [HideInInspector]
    public bool CanMove = true;

    public void Start()
    {
        OnCreated?.Invoke(this);
    }
}
