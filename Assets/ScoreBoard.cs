using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WinText;
    public GameObject WinScreen;
    private int wait = 3;
    public static ScoreBoard instance;
    [SerializeField] TextMeshProUGUI KillCounter_TMP1;
    [SerializeField] TextMeshProUGUI KillCounter_TMP2;
    [HideInInspector] public int KillCount1;
    [HideInInspector] public int KillCount2;
   
    private void Awake()
    {
        WinScreen.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateKillCounterUI()
    {
        KillCounter_TMP1.text = ($"{KillCount1} / {"5"}".ToString());
        KillCounter_TMP2.text = ($"{KillCount2} / {"5"}".ToString());
    }
    IEnumerator ExampleCoroutine()
    {
        WinScreen.SetActive(true);
        
        yield return new WaitForSeconds(wait);
         SceneLoader.LoadScene("MainMenu");
    }
    public void Update()
    {
         if (KillCount1 >= 5)
    {
            WinText.SetText("Player 2 Winner");

           StartCoroutine(ExampleCoroutine());

           

    }
        if (KillCount2 >= 5)
        {
            WinText.SetText("Player 1 Winner");
            StartCoroutine(ExampleCoroutine());
        }
    }
   
}
