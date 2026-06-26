using UnityEngine;

 public class PlayerBlock : WeaponBase
{
    public GameObject block;
    private int PlayerID => Player.PlayerID;
    public bool isBlocking = false;
    public string InputString = $"Block - Player";
    void Update()
    {
        if (Input.GetButton($"{InputString} {PlayerID}"))
        {
            block.SetActive(true);
           isBlocking = true;
        }
        else
        {
            block.SetActive(false);
            isBlocking = false;
        }
    }
    
  
}
