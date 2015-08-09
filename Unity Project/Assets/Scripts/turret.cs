using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {
	
	public Transform target;
	public float turretSpeed;
	public float fireRate;
	public float fireBallHeight;
	public GameObject fireBall;
	public float range;
	float distance;
	private float _lastShotTime = float.MinValue;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Rotate turret to look at player.
		Vector3 relativePos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos); 
		rotation.x = 0;
		rotation.z = 0;
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turretSpeed);
		
		//Fire at player when in range.
		distance = Vector3.Distance (transform.position, target.position);
		
		if (distance < range && Time.time > _lastShotTime + (3.0f / fireRate)) {
			_lastShotTime = Time.time;
			//print Time.time;
			launchFireBall();
		}

	}

	
	void launchFireBall()
	{
		Vector3 position = new Vector3(transform.position.x, transform.position.y + fireBallHeight, transform.position.z);
		Instantiate(fireBall, position, transform.rotation);
	}
}