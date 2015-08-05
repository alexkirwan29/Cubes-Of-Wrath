using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{
    public float forwardSpeed = 8f;
    public float turnRate = 45f;

    CharacterController controller;

    void Start()
    {
        GameCamera.instance.SetTarget(transform);
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Rotate the character then move it forward.
        transform.Rotate(Vector3.up, Input.GetAxisRaw("Horizontal") * turnRate * Time.deltaTime);
        Vector3 move = transform.forward * forwardSpeed * Time.deltaTime;
        move.y = -Physics.gravity.magnitude;
        controller.Move(move);
    }
}