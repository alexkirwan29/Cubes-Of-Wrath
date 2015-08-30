using UnityEngine;
using System.Collections;

public class wallDetect : MonoBehaviour {

    public float rightLimit = 2.5f;
    public float leftLimit = 1.0f;
    public float speed = 2.0f;
    private int direction = 1;

    void Update()
    {
        if (transform.position.x > rightLimit)
        {
            direction = -1;
        }
        else if (transform.position.x < leftLimit)
        {
            direction = 1;
        }

    }


         
}
