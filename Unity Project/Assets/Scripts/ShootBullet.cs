using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {
    public Rigidbody projectile;
    public float nextShot = 0;
    public float fireDelay = 0.5f;
    public float range;
    public float distance;
    public Transform target;

    AudioSource sound;
    
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }
        
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (distance < range && Time.time > nextShot)
        {
            nextShot = Time.time + fireDelay;
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            clone.velocity = transform.TransformDirection(Vector3.forward * 10);
            sound.Play();
        }
    }
 }