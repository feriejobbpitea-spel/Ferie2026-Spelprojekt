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
        // Återställ spelarens position till 0, 0
        Player.transform.position = Vector3.zero;

        Player.SetActive(true);
    }
}
