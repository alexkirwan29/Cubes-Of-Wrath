using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startTimer : MonoBehaviour
{

    public float timeLeft = 600;
    Text countdownText;

    void Update()
    {
        timeLeft -= Time.deltaTime;

        {
            countdownText = GetComponent<Text>();
            countdownText.text = string.Format("Time Left:  {0}.{00:00.00}", (int)(timeLeft / 60), timeLeft % 60);
        }

        if (timeLeft < 0)
        {
            countdownText.text = (" sfdg");
        }
    }



}
