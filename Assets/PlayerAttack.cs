using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AudioClip[] AttackSoundClip;
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
        attack();
    }
    private void attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);

        if (Random.Range(0, 11) < 1)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(AttackSoundClip, transform, 1f);
        }
            
    }
}
