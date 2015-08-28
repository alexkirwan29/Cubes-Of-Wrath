using UnityEngine;
using System.Collections;

public class wallDetect : MonoBehaviour {

    public float leftMost;
    public float rightMost;

	// Use this for initialization
	void Start () {

        RaycastHit hit;
        float distanceToGround = 0;

        if (Physics.Raycast(transform.position, Vector3.left, out hit))
            distanceToGround = hit.distance;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
