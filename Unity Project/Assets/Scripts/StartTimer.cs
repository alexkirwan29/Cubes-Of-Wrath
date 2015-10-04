using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour {

    public float timeLeft = 3;
    Text startTimer;

    void Update ()
    {
        timeLeft -= Time.deltaTime;

        {
            startTimer = GetComponent<Text>();
            startTimer.text = string.Format("{1:0}", (int)(timeLeft / 60), timeLeft % 60);
        }

        if (timeLeft < 0)
        {
            Debug.Log("the timer fuken finshed. k");
            
            
        }
                

    }    
           

}
