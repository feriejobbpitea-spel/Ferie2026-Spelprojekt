using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
  

    public void Map1()
    {
        SceneLoader.LoadScene("Map 1");
    }

    public void Map2()
    {
        SceneLoader.LoadScene("Map 2");
    }

    public void Map3() 
    {
        SceneLoader.LoadScene("Map 3");
    }


}