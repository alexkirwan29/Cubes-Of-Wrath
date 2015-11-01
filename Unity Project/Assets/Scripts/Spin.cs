using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	
	void Update () {
        transform.Rotate(new Vector3(90, 90, 90) * Time.deltaTime);
	
	}
}
