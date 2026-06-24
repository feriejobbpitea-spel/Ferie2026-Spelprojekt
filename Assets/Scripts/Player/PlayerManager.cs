using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private List<Player> PlayerPrefabs = new();

    // Call-a från "character select skärmen" för att välja en prefab för varje spelare, så att vi kan spawna rätt karaktär när spelet startar
    public static int player1ID = 0;
    public static int player2ID = 0;

    // Key = Styrande spelaren
    // Value = Prefab för spelaren
    public Dictionary<Player, Player> SpawnedPlayers = new();

    public static Action OnRoundPrepared;
    public static Action OnRoundStart;

    protected override void Awake()
    {
        base.Awake();

        SpawnedPlayers.Clear();

        SpawnPlayer(PlayerPrefabs[player1ID], 1);
        SpawnPlayer(PlayerPrefabs[player2ID], 2);
    }

    private void Start()
    {
        StartCoroutine(NewRoundManager());
    }

    public void SpawnPlayer(Player playerPrefab, int ownerID) 
    {
        Vector3 spawnPosition = Vector2.zero;

        // Skapa en ny spelare
        var newPlayer = GameObject.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        newPlayer.name = $"Player {ownerID} ({playerPrefab.name})";

        // Assigna spelarens ID så att den vet vilken input den ska lyssna på
        newPlayer.PlayerID = ownerID;

        SpawnedPlayers.Add(newPlayer, playerPrefab);
    }

    public void OnPlayerDeath(GameObject Player)
    {
        Player.SetActive(false);
        StartCoroutine(NewRoundManager());
    }
   
    private IEnumerator NewRoundManager() 
    {
        PrepareNextRound();

        if(BeginRoundDialogue.Instance == null) 
        {
            Debug.LogError($"Lägg till scriptet '{nameof(BeginRoundDialogue)}' i scenen.");
        }

        // Spela ljud för båda spelarna
        foreach (var player in SpawnedPlayers)
        {
            if (player.Key == null)
                continue;
            float timeToWait = BeginRoundDialogue.Instance.PlayAudio(player.Key.PlayerID);
            yield return new WaitForSeconds(timeToWait);
        }

        yield return new WaitForSeconds(1);
        StartRound();
    }

    /// <summary>
    /// Förbered för rundan som kommer
    /// </summary>
    private void PrepareNextRound()
    {
        // Återställ spelaren
        foreach (var player in SpawnedPlayers)
        {
            player.Key.gameObject.SetActive(false);
         
            // Slumpa deras position
            player.Key.transform.position = GetRandomSpawnPosition(player.Key.PlayerID);

            player.Key.GetComponent<PlayerHealth>().ResetPlayerHealth();

            player.Key.gameObject.SetActive(true);

            player.Key.CanMove = false;
        }

        OnRoundPrepared?.Invoke();
    }

    /// <summary>
    /// Startar rundan på riktigt
    /// </summary>
    private void StartRound() 
    {
        foreach (var player in SpawnedPlayers)
        {
            player.Key.CanMove = true;
        }

        OnRoundStart?.Invoke();   
    }

    private Vector3 GetRandomSpawnPosition(int playerID) 
    {
        Vector2 spawnPosition = Vector2.zero;

        // Hitta all objekt med scriptet "spawnpoint" i scenen
        var allSpawnPoints = GameObject.FindObjectsByType<Spawnpoint>().Where(x => x.ID == playerID).ToList();
        if(allSpawnPoints.Count <= 0)
            return spawnPosition;

        // Välj en slumpmässig en
        var randomSpawnPoint = allSpawnPoints.GetRandom();


        // Om det finns en spawnpoint så sätter vi spawn position till den
        if (randomSpawnPoint != null)
            spawnPosition = randomSpawnPoint.transform.position;

        return spawnPosition;
    }
}
