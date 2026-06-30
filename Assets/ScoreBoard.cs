using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WinText;
    public GameObject WinScreen;
    private int wait = 3;
    public static ScoreBoard instance;
    //[SerializeField] TextMeshProUGUI KillCounter_TMP1;
    //[SerializeField] TextMeshProUGUI KillCounter_TMP2;

    [SerializeField] private Slider ScoreCounter_1;
    [SerializeField] private Slider ScoreCounter_2;

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

    private void Start()
    {
        UpdateKillCounterUI();
    }

    private void OnEnable()
    {
        PlayerHealth.OnKubbkingDied += KubbkingDied;
    }

    private void OnDisable()
    {
        PlayerHealth.OnKubbkingDied -= KubbkingDied;
    }


    /// <summary>
    /// Koden inuti körs om kubb-kungen dör
    /// </summary>
    private void KubbkingDied()
    {

    }

    public void UpdateKillCounterUI()
    {
        //KillCounter_TMP1.text = KillCount1.ToString();
        //KillCounter_TMP2.text = KillCount2.ToString();
        ScoreCounter_1.value = 5 - KillCount1;
        ScoreCounter_2.value = 5 - KillCount2;


    }
    IEnumerator ExampleCoroutine()
    {
        Time.timeScale = 0;
        WinScreen.SetActive(true);
        
        yield return new WaitForSecondsRealtime(wait);
        Time.timeScale = 1;
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
