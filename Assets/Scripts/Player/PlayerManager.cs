using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private List<Player> PlayerPrefabs = new();
    [SerializeField] private bool SkipIntroCutscene;
    
    // Call-a från "character select skärmen" för att välja en prefab för varje spelare, så att vi kan spawna rätt karaktär när spelet startar
    public static int player1ID = 3;
    public static int player2ID = 1;
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;
    [SerializeField] private AudioClip damageSoundClip;
    // Key = Styrande spelaren
    // Value = Prefab för spelaren
    public Dictionary<Player, Player> SpawnedPlayers = new();

    public static Action OnRoundPrepared;
    public static Action OnRoundStart;

    protected override void Awake()
    {
        base.Awake();

        SpawnedPlayers.Clear();

        countdownDisplay.text = string.Empty;

      /*  if (player1ID < 0)
            player1ID = UnityEngine.Random.Range(0, PlayerPrefabs.Count);

        if (player2ID < 0)
            player2ID = UnityEngine.Random.Range(0, PlayerPrefabs.Count);*/

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
        StartCoroutine(PlayFinisher(Player));
    }

    private IEnumerator PlayFinisher(GameObject Player) 
    {
        Time.timeScale = 0.2F;
        Player.SetActive(false);
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;

        yield return new WaitForSeconds(3);
        StartCoroutine(NewRoundManager());
    }

    private IEnumerator NewRoundManager() 
    {
        PrepareNextRound();

        if(BeginRoundDialogue.Instance == null) 
        {
            Debug.LogError($"Lägg till scriptet '{nameof(BeginRoundDialogue)}' i scenen.");
        }

        if (SkipIntroCutscene)
        {
            Debug.Log($"Skippar intro voicelines! Ändra detta på {nameof(PlayerManager)}'s gameObject.");
        }
        else 
        { 
            // Spela ljud för båda spelarna
            foreach (var player in SpawnedPlayers)
            {
                if (player.Key == null)
                    continue;
                float timeToWait = BeginRoundDialogue.Instance.PlayAudio(player.Key.PlayerID);
                yield return new WaitForSeconds(timeToWait);
            }
        }

        yield return new WaitForSeconds(1);

        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform, 1f);

        countdownDisplay.gameObject.SetActive(true);

        int realCountdownTime = countdownTime;
        while (realCountdownTime > 0)
            {
            countdownDisplay.transform.localScale = Vector3.zero;
            countdownDisplay.transform.DOScale(Vector3.one, .3F);
                countdownDisplay.text = realCountdownTime.ToString();

                yield return new WaitForSeconds(.5f);

            realCountdownTime--;

        }


        countdownDisplay.transform.localScale = Vector3.zero;
        countdownDisplay.transform.DOScale(Vector3.one, .5F);
        countdownDisplay.text = "FIGHT";


            yield return new WaitForSeconds(1f);

            countdownDisplay.gameObject.SetActive(false);

        
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
