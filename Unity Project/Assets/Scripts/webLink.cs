using UnityEngine;
using System.Collections;

public class webLink : MonoBehaviour {

    public string URL;

	// Use this for initialization
	void Start () {
        Application.OpenURL(URL);
	}
	

}
