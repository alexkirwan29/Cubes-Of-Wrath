using UnityEngine;
using System.Collections;

public class BulletAlive : MonoBehaviour {
    float bulletAlive = 1.0f;
	void Update () {
        Destroy(gameObject, bulletAlive);
	}
}
