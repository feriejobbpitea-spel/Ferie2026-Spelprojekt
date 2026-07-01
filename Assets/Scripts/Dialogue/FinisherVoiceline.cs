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

    private void OnPlayerDied(PlayerHealth deadPerson)
    {
        var winningPlayer = PlayerManager.Instance.SpawnedPlayers.FirstOrDefault(x => x.Key.PlayerID != deadPerson.PlayerID);

        if(winningPlayer.Key == null) 
        {
            return;
        }

        if (!Voicelines.ContainsKey(winningPlayer.Value))
        {
            return;
        }
        Debug.Log($"Winning player: {winningPlayer}");

        DialoguePlayer.Instance.NewDialogue(Voicelines[winningPlayer.Value].GetRandom(), winningPlayer.Key.transform);
    }
}
