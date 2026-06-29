using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
  

    public void Map1()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Map2()
    {
        SceneManager.LoadScene("Map 2");
    }


}