using System;
using UnityEngine;

public class PlayerJumpVFX : MonoBehaviour
{
    [SerializeField]
    private GameObject jumpVFX;
    
    private PlayerMovement playerMovement;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerMovement.OnJumped += PlayJumpVFX;
    }

    private void OnDisable()
    {
        playerMovement.OnJumped -= PlayJumpVFX;
    }

    private void PlayJumpVFX()
    {
        var newJumpVFX = GameObject.Instantiate(jumpVFX, transform.position, Quaternion.identity);
        GameObject.Destroy(newJumpVFX, 1);
    }

    
}
