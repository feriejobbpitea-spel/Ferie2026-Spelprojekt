using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;


public class MultiplayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup TargetGroup;
    [SerializeField] private float RadiusSize = 3;

    private void LateUpdate()
    {
        // Hitta alla nya spelare
        TargetGroup.Targets.Clear();
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            TargetGroup.Targets.Add(new CinemachineTargetGroup.Target()
            {
                Object = player.transform,
                Radius = RadiusSize
            });
        }
    }

    private void OnValidate()
    {
        if (TargetGroup == null)
        {
            TargetGroup = GetComponent<CinemachineTargetGroup>();
        }
        TargetGroup.Targets.Clear();
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            TargetGroup.Targets.Add(new CinemachineTargetGroup.Target()
            {
                Object = player.transform,
                Radius = RadiusSize
            });
        }
    }
}
