using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    private void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        
    }
    private void attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }
}
