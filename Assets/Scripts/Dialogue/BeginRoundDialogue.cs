using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;

public class BeginRoundDialogue : Singleton<BeginRoundDialogue>
{
    [SerializeField] private SerializedDictionary<Player, List<AudioWithSubtitles>> AudioToPlay = new();

    protected override void Awake()
    {
        base.Awake();
    }

    public float PlayAudio(int playerID) 
    {
        var spawnedPlayers = PlayerManager.Instance.SpawnedPlayers;
        var playerWithCorrectID = spawnedPlayers.FirstOrDefault(x => x.Key.PlayerID == playerID);

        if (!AudioToPlay.ContainsKey(playerWithCorrectID.Value))
            return 0;

        // Slumpa fram vilken audio vi vill spela från listan
        // Baseras på vilken karaktär de spelar som
        AudioWithSubtitles audioToPlay = AudioToPlay[playerWithCorrectID.Value].GetRandom();

        DialoguePlayer.Instance.NewDialogue(audioToPlay, playerWithCorrectID.Key.transform);
        return audioToPlay.AudioClip.length;
    }
}
