using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

public class PlayerID_Sprite : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float SmoothSpeed;
    private Transform target;

    private void OnEnable()
    {
        Player.OnCreated += OnPlayerCreated;
    }

    private void OnDisable()
    {
        Player.OnCreated -= OnPlayerCreated;
    }

    private void OnPlayerCreated(Player player)
    {
        if (player.PlayerID != ID)
            return;
        target = player.transform;
    }

    private void Update()
    {
        if (target == null)
            return;
        transform.position = Vector3.Lerp(transform.position, target.position + Offset, Time.deltaTime * SmoothSpeed);
    }
}
