using TMPro;
using UnityEngine;

public class PlayerHurt_Feedback : MonoBehaviour
{
    [SerializeField] private ParticleSystem ParticlePrefab;

    [SerializeField] private TMP_Text TextPrefab;
    [SerializeField] private float RemoveAfterTime;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float RandomOffset;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerHurt += PlayerHealth_OnPlayerHurt;
    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerHurt -= PlayerHealth_OnPlayerHurt;
    }

    private void PlayerHealth_OnPlayerHurt(PlayerHurtPayload payload)
    {
        var newParticles = GameObject.Instantiate(ParticlePrefab, payload.Victim.transform.position, Quaternion.identity);
        Destroy(newParticles, RemoveAfterTime);

        var newText = GameObject.Instantiate(TextPrefab, payload.Victim.transform.position + Offset + Random.insideUnitSphere * RandomOffset, Quaternion.identity);
        
        newText.text = $"-{payload.DamageTaken.ToString()}";
     
        Destroy(newText.gameObject, RemoveAfterTime);
    }
}