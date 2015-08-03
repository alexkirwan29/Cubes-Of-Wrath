using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    [Tooltip("The speed (M/s) that the player moves along it's X axis (sideways)")]
	public float speed = 1.0f;
    [Tooltip("The speed that the player moves along it's Z axis (forward)")]
    public float fwdSpeed = 5f;

    Rigidbody rigidbody;

    void Start()
    {
        // Get the rigidbody component and stuff in-to the rigidbody varible
        // then tell the camera to follow this player

        rigidbody = GetComponent<Rigidbody>();
        GameCamera.instance.SetTarget(transform);
    }

    void FixedUpdate()
    {
        // Create a vector for the direction we want to move. then translate
        // the rigidbody by the direction vector multiplied by time.delta time.

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, fwdSpeed);
        rigidbody.MovePosition(rigidbody.position + move * Time.deltaTime);
    }
}