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

    private int KillCount1;
    private int KillCount2;

    public static Action OnPlayerWonRound;

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

    public void Player1_WonRound()
    {
        KillCount1++;
        UpdateKillCounterUI();
        OnPlayerWonRound?.Invoke();
    }

    public void Player2_WonRound() 
    {
      
        KillCount2++;
        UpdateKillCounterUI();
        OnPlayerWonRound?.Invoke();
    }
   
}
