using System.Collections;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
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
        Player.SetActive(true);
    }
}
