using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinisherVoiceline : MonoBehaviour
{
    public SerializedDictionary<Player, List<AudioWithSubtitles>> Voicelines = new();

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += OnPlayerDied;
    }

   

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied(PlayerHurtPayload deadPerson)
    {
        Player winningPlayer = PlayerManager.Instance.SpawnedPlayers.FirstOrDefault(x => x.Key != deadPerson.Victim).Key;

        if(winningPlayer == null) 
        {
            return;
        }

        if (!Voicelines.ContainsKey(winningPlayer))
        {
            return;
        }

        DialoguePlayer.Instance.NewDialogue(Voicelines[winningPlayer].GetRandom(), winningPlayer.transform);
    }
}
