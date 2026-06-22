using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selectedskin;
    public GameObject Player;
    [SerializeField] private int PlayerID = 0;

    private Sprite PlayerSprite;
    void Start()
    {
       
        {
            PlayerSprite = selectedskin.GetComponent<SpriteRenderer>().sprite;

            Player.GetComponent<SpriteRenderer>().sprite = PlayerSprite;
        }
    }

  
}
