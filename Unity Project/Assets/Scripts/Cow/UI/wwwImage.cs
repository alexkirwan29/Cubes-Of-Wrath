using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class wwwImage : MonoBehaviour {

	public string url = "http://www.imagelocation.com";

	IEnumerator Start() {
		//Start download of the given URL
		WWW www = new WWW (url);

		//Wait for download to complete
		yield return www;

		// assign texture

		RawImage image = GetComponent<RawImage> ();
		image.texture = www.texture;
	}
}
