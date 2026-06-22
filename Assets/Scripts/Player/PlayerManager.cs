using System.Collections;
using System.Collections.Generic;
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
        // Skapa en ny spelare
        var newPlayer = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

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
