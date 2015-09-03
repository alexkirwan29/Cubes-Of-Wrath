using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class countdownTimer : MonoBehaviour {

    public float timeLeft = 500;
    Text countdownText;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if ( timeLeft < 0)
        {
            Debug.Log("YOU RAN OUT OF TIME :(");

        }

        {
            countdownText = GetComponent<Text>();
            countdownText.text = "Time Left : " + timeLeft;
        }


    }



}
