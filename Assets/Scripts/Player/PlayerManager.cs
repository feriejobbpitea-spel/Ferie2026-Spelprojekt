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

    protected override void Awake()
    {
        base.Awake();

        SpawnPlayer(PlayerPrefabs[player1ID], 1);
        SpawnPlayer(PlayerPrefabs[player2ID], 2);
    }

    public void SpawnPlayer(Player playerPrefab, int ownerID) 
    {
        // Hitta all objekt med scriptet "spawnpoint" i scenen
        var allSpawnPoints = GameObject.FindObjectsByType<Spawnpoint>();
        // Välj en slumpmässig en
        var randomSpawnPoint = allSpawnPoints.ElementAtOrDefault(new System.Random().Next() % allSpawnPoints.Count());

        Vector2 spawnPosition = Vector2.zero;

        // Om det finns en spawnpoint så sätter vi spawn position till den
        if (randomSpawnPoint != null)
            spawnPosition = randomSpawnPoint.transform.position;

        // Skapa en ny spelare
        var newPlayer = GameObject.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

        // Assigna spelarens ID så att den vet vilken input den ska lyssna på
        newPlayer.PlayerID = ownerID;
    }

    public void OnPlayerDeath(GameObject Player)
    {
        Player.SetActive(false);
        StartCoroutine(Wait(Player));
    }
   
    private IEnumerator Wait(GameObject Player) 
    {
        yield return new WaitForSeconds(5);
        OnPlayerRespawn(Player);
    }
   
    private void OnPlayerRespawn(GameObject Player)
    {
        // Återställ spelarens position till 0, 0
        Player.transform.position = Vector3.zero;

        Player.SetActive(true);
    }
}
