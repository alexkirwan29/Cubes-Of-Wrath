/*using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class Showad : MonoBehaviour
{
    public void Awake()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("76230");
        }
        else
        {
            Debug.Log("Platform not supported");
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 300, 125), Advertisement.IsReady() ? "Show Ad" : "Waiting..."))
        {
            // Show with default zone, pause engine and print result to debug log
            Advertisement.Show(null, new ShowOptions
            {
                resultCallback = result => {
                    Debug.Log(result.ToString());
                }
            });
        }
    }
}*/