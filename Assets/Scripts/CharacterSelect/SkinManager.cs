using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public int PlayerID;    
    public Image sr;
    public List<Sprite> skins = new List<Sprite>();
    private int selectedSkin = 0;

    public void NextOption()
    {
        selectedSkin = selectedSkin + 1;
        if (selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }

        sr.sprite = skins[selectedSkin];
    }

    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count -1;
        }

        sr.sprite = skins[selectedSkin];
    }

    public void PlayGame()
    {
        if (PlayerID == 1)
        {
            PlayerManager.player1ID = selectedSkin;

        }

        if (PlayerID == 2)
        {
            PlayerManager.player2ID = selectedSkin;

        }

        //PrefabUtility.SaveAsPrefabAsset(playerskin, "Assets/selectskin.prefab");
        //SceneManager.LoadScene("MainGame");
        //SceneLoader.LoadScene("MainGame");
    }
}
