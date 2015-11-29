using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 20;

    // Update is called once per frame
    void Update()
    {
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
     
    }
}
