using UnityEngine;
using System.Collections;

public class destroyObject : MonoBehaviour
{


    public Score score;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            score.AddScore(50);
        }
    }


}
