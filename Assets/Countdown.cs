using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Countdown : MonoBehaviour
{
    
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;
    private void Start()
    {
        StartCoroutine(CountdownToStart());
       
    }

 


    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
            
        }
       
        countdownDisplay.text = "FIGHT";


        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
        
    }
}
